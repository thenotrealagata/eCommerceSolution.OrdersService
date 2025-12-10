using DataAccess.Repositories;
using DataAccess.RepositoryContracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("MongoDb")!
                .Replace("$MONGO_HOST", Environment.GetEnvironmentVariable("MONGODB_HOST"))
                .Replace("$MONGO_PORT", Environment.GetEnvironmentVariable("MONGODB_PORT"));

            services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
            services.AddScoped<IMongoDatabase>(provider =>
            {
                IMongoClient client = provider.GetRequiredService<IMongoClient>();
                return client.GetDatabase("OrdersDb");
            });

            services.AddScoped<IOrdersRepository, OrdersRepository>();

            return services;
        }
    }
}
