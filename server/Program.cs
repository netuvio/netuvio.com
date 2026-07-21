using System.Text.Json.Serialization;
using Asp.Versioning;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using server.Infrastructure.Persistence;
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

        DotNetEnv.Env.Load("../.env");

        // Add services to the container.

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(
                    // Use PascalCase for enum values in JSON
                    new JsonStringEnumConverter()
                );
            });
        
        builder.Services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = false;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
        
        builder.Services.AddHttpContextAccessor();
        
        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        
        builder.Services.AddOpenApi(options =>
        {
            // Generate OperationID in api schema from method name
            options.AddOperationTransformer((operation, context, _) =>
            {
                // Check for [EndpointName]
                var endpointNameMetadata = context.Description.ActionDescriptor.EndpointMetadata
                    .OfType<IEndpointNameMetadata>()
                    .LastOrDefault();
            
                if (endpointNameMetadata != null)
                {
                    operation.OperationId = endpointNameMetadata.EndpointName;
                }
                // Fallback to routing attribute name (e.g., [HttpGet(Name = "MyName")])
                else if (!string.IsNullOrWhiteSpace(context.Description.ActionDescriptor.AttributeRouteInfo?.Name))
                {
                    operation.OperationId = context.Description.ActionDescriptor.AttributeRouteInfo.Name;
                }
                // Fallback to method name
                else if (context.Description.TryGetMethodInfo(out var methodInfo))
                {
                    operation.OperationId = methodInfo.Name;
                }
            
                return Task.CompletedTask;
            });
        });
        
        builder.Services.AddHttpClient();
        
        // Connect to DB
        var connectionString = new NpgsqlConnectionStringBuilder
        {
            Host = Environment.GetEnvironmentVariable("DB_HOST"),
            Database = Environment.GetEnvironmentVariable("DB_NAME"),
            Username = Environment.GetEnvironmentVariable("DB_USER"),
            Password = Environment.GetEnvironmentVariable("DB_PASSWORD")
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

        // pridani X-Powered-By
        Application.Use(async (context, next) => {
            context.Response.Headers.Append("X-Powered-By", "ASP.NET");
            await next.Invoke();
        });


        Application.MapControllers();

        //app.MapFallbackToFile("/index.html");

        Logger = Application.Logger;


        Application.Run();
    }
}