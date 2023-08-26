using Contracts;
using LoggerService;
using MySimpleDatabaseDriver;
using Repository;

namespace my_simple_web_api.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => 
            {

            });
        }

        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureMySimpleDatabaseConnection(this IServiceCollection services, IConfiguration config)
        {
            string host = config["MySimpleDatabaseConnection:host"];
            int port = int.Parse(config["MySimpleDatabaseConnection:port"]);

            // Change to a non-generic type
            var dbClient = new MySimpleDatabaseClient(host, port);

            services.AddSingleton(dbClient);
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}