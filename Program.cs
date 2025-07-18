using Microsoft.EntityFrameworkCore;
using demo_store_product_service.Data;
using demo_store_product_service.Config;
using Microsoft.Extensions.Options; // Required for IOptions

var builder = WebApplication.CreateBuilder(args);

// Bind the custom configuration section to your C# class
builder.Services.Configure<DatabaseSettingsConfig>(
    builder.Configuration.GetSection("DatabaseSettingsConfig"));

// Inject IConfiguration and IOptions<DatabaseSettingsConfig> directly
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    // Resolve IOptions<DatabaseSettingsConfig> from the serviceProvider
    var dbSettings = serviceProvider.GetRequiredService<IOptions<DatabaseSettingsConfig>>().Value;

    // Construct the connection string using the settings
    var connectionString = dbSettings.ToConnectionString();

    options.UseSqlServer(connectionString);
});

// Define a CORS policy name at a scope where it's accessible to both AddCors and UseCors
const string MyCorsPolicy = "_myCorsPolicy";

// Add services to the container.
builder.Services.AddControllers(); // <--- ADD THIS LINE TO REGISTER CONTROLLER SERVICES

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure CORS services
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyCorsPolicy,
                      policy =>
                      {
                          // Get the CORS settings directly from configuration.
                          // This will load from appsettings.json, appsettings.Development.json,
                          // and be overridden by environment variables/Azure App Settings.
                          var corsSettings = builder.Configuration.GetSection("CorsSettingsConfig").Get<CorsSettingsConfig>();

                          if (corsSettings != null && corsSettings.AllowedOrigins != null && corsSettings.AllowedOrigins.Length > 0)
                          {
                              // If origins are specified, use them.
                              policy.WithOrigins(corsSettings.AllowedOrigins)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials(); // Allow credentials if your frontend sends them (e.g., cookies, auth headers)
                          }
                          else
                          {
                              // Fallback for development if no origins are explicitly configured,
                              // or a warning for production.
                              if (builder.Environment.IsDevelopment())
                              {
                                  // In development, if no origins are specified, allow any for ease of testing.
                                  // This is less secure and should NOT be used in production.
                                  policy.AllowAnyOrigin()
                                        .AllowAnyHeader()
                                        .AllowAnyMethod()
                                        .AllowCredentials();
                                  Console.WriteLine("WARNING: CORS policy allowing any origin in Development environment due to no specific origins configured.");
                              }
                              else
                              {
                                  // In production, if no origins are configured, it's safer to be restrictive.
                                  // The policy will effectively not allow any cross-origin requests unless explicitly added.
                                  Console.WriteLine("WARNING: No CORS origins configured in 'CorsSettingsConfig:AllowedOrigins'. Production CORS policy might be too restrictive or needs explicit configuration.");
                                  // You could also throw an exception here if misconfiguration is critical:
                                  // throw new InvalidOperationException("CORS origins must be configured for production environment.");
                              }
                          }
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS middleware -- IMPORTANT: This must be placed after UseRouting and before UseEndpoints/MapControllers
app.UseCors(MyCorsPolicy);

// map controller routes
app.MapControllers();

app.Run();
