using System.Text.Json.Serialization;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using server.Data;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace server;

public static class Program {
    public static ILogger Logger { get; private set; } = null!;
    public static WebApplication Application { get; private set; } = null!;


#if DEBUG
    public static readonly bool DevelopmentMode = true;
#else
        public static readonly bool DevelopmentMode = false;
#endif


    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        string[] candidates = [
            ".env",
            "../.env",
            Path.Combine(AppContext.BaseDirectory, ".env"),
            Path.Combine(AppContext.BaseDirectory, "../.env"),
            "/app/.env",
            "/.env"
        ];

        bool envLoaded = false;
        foreach (var path in candidates) {
            try {
                if (File.Exists(path)) {
                    DotNetEnv.Env.Load(path);
                    Console.WriteLine($"[Env] Loaded environment from: {Path.GetFullPath(path)}");
                    envLoaded = true;
                    break;
                }
            } catch (Exception ex) {
                Console.WriteLine($"[Env] Failed to load {path}: {ex.Message}");
            }
        }

        if (!envLoaded) {
            Console.WriteLine("[Env] Notice: No .env file loaded from candidate paths.");
        }

        // Add services to the container.

        builder.Services.AddControllers()
            .AddJsonOptions(options => {
                options.JsonSerializerOptions.Converters.Add(
                    // Use PascalCase for enum values in JSON
                    new JsonStringEnumConverter()
                );
            });

        builder.Services.AddApiVersioning(options => {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = false;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options => {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        builder.Services.AddHttpContextAccessor();

        builder.Services.ConfigureHttpJsonOptions(options => {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddOpenApi(options => {
            // Generate OperationID in api schema from method name
            options.AddOperationTransformer((operation, context, _) => {
                // Check for [EndpointName]
                var endpointNameMetadata = context.Description.ActionDescriptor.EndpointMetadata
                    .OfType<IEndpointNameMetadata>()
                    .LastOrDefault();

                if (endpointNameMetadata != null)
                    operation.OperationId = endpointNameMetadata.EndpointName;
                // Fallback to routing attribute name (e.g., [HttpGet(Name = "MyName")])
                else if (!string.IsNullOrWhiteSpace(context.Description.ActionDescriptor.AttributeRouteInfo?.Name))
                    operation.OperationId = context.Description.ActionDescriptor.AttributeRouteInfo.Name;
                // Fallback to method name
                else if (context.Description.TryGetMethodInfo(out var methodInfo))
                    operation.OperationId = methodInfo.Name;

                return Task.CompletedTask;
            });
        });

        builder.Services.AddHttpClient();

        // Connect to DB
        var rawHost = Environment.GetEnvironmentVariable("DB_HOST");
        var host = rawHost;
        var port = 5432;
        if (!string.IsNullOrEmpty(host) && host.Contains(':')) {
            var parts = host.Split(':');
            host = parts[0];
            if (int.TryParse(parts[1], out var parsedPort)) {
                port = parsedPort;
            }
        } else if (int.TryParse(Environment.GetEnvironmentVariable("DB_PORT"), out var envPort)) {
            port = envPort;
        }

        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        var dbUser = Environment.GetEnvironmentVariable("DB_USER");
        var dbPass = Environment.GetEnvironmentVariable("DB_PASSWORD");

        Console.WriteLine($"[DB] Host: '{host}', Port: {port}, Database: '{dbName}', User: '{dbUser}', PasswordSet: {!string.IsNullOrEmpty(dbPass)}");

        var connectionString = new NpgsqlConnectionStringBuilder {
            Host = host,
            Port = port,
            Database = dbName,
            Username = dbUser,
            Password = dbPass
        };

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString.ConnectionString));


        Application = builder.Build();

        Application.UseDefaultFiles();
        Application.MapStaticAssets();

        // Configure the HTTP request pipeline.
        //if (Application.Environment.IsDevelopment()) Application.MapOpenApi();


        //app.UseHttpsRedirection();

        Application.UseAuthorization();
        Application.UseCors();


        Application.MapControllers();

        //app.MapFallbackToFile("/index.html");

        Logger = Application.Logger;


        Application.Run();
    }
}
