namespace demo_store_product_service.Config;

public class CorsSettingsConfig
{
    // This array will hold the list of allowed origins.
    // It will be populated from the "AllowedOrigins" array in appsettings.json
    // or from environment variables/Azure App Settings.
    public string[] AllowedOrigins { get; set; } = Array.Empty<string>();
}
