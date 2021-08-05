
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDBDemo.DataBaseContext;
using MongoDBDemo.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDBDemo
{
    public static class ServicesConfiguration
    {
        private static IServiceCollection _services { get; set; }
        private static IConfiguration _configuration { get; set; }
        public static void ConfigureSerices(IServiceCollection services,IConfiguration configuration)
        {
            _services = services;
            _configuration = configuration;
            ConfigureDBContext();
            ConfigureDI();
            ConfigureAPI();
        }

        private static void ConfigureAPI()
        {
            _services.AddControllers();
            _services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MongoDBDemo", Version = "v1" });
            });
        }
       
        private static void ConfigureDBContext()
        {

            var dbSettings = new DatabaseSettings();
            _configuration.Bind(dbSettings.SectionName, dbSettings);      
            _services.AddSingleton(dbSettings);
            _services.AddSingleton<IDBContext,DBContext>();
        }
        private static void ConfigureDI()
        {
            _services.AddScoped<IProjectRepo, ProjectRepo>();

        }

       
    }
}
