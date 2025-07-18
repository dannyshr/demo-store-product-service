
namespace demo_store_product_service.Config; 

public class DatabaseSettingsConfig
{
    public string Server { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string OtherParams { get; set; } = string.Empty;

    // Method to construct the full connection string
    public string ToConnectionString()
    {
        // Basic validation for critical components
        if (string.IsNullOrWhiteSpace(Server) ||
            string.IsNullOrWhiteSpace(Database) ||
            string.IsNullOrWhiteSpace(UserId) ||
            string.IsNullOrWhiteSpace(Password))
        {
            throw new InvalidOperationException("Database connection string components (Server, Database, UserId, Password) must be provided.");
        }

        return $"Server={Server};Initial Catalog={Database};User ID={UserId};Password={Password};{OtherParams}";
    }
}