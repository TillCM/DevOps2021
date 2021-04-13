using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using team_reece.Models;
using Microsoft.EntityFrameworkCore;


namespace team_reece
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine(EnvironmentHelper.GenerateDBConnectionFromEnv());
            services.AddDbContext<teamfuContext>(options=> options.UseSqlServer(EnvironmentHelper.GenerateDBConnectionFromEnv()));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "team_reece", Version = "v1" });
            });            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                //app.UseEnvironnmentVariables();
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "team_reece v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class EnvironmentHelper
    {        
        public static string GetEnvironmentVariableValue(string variableName, string defaultValue)
        {
            var variableValue = Environment.GetEnvironmentVariable(variableName);
            
            if (!string.IsNullOrWhiteSpace(variableValue)) 
                return variableValue;

            if (defaultValue == null)
            {
                var errorMessage = $"Environment variable '{variableName}` is required.";
                Console.WriteLine(errorMessage);
                throw new ArgumentNullException(variableName, errorMessage);
            }

            return defaultValue;
        }

        public static string GenerateDBConnectionFromEnv()
        {
            string host = GetEnvironmentVariableValue("DATABASE_SERVER", null);
            string port = GetEnvironmentVariableValue("DATABASE_PORT", "1433");
            string userid = GetEnvironmentVariableValue("DATABASE_USER", null);
            string password = GetEnvironmentVariableValue("DATABASE_PASSWORD", null);
            string database = GetEnvironmentVariableValue("DATABASE_NAME", null);
            return $"Data Source={host},{port};Database={database};User Id={userid};Password={password};";
        }

        //  public static string GenerateDBConnectionFromEnv()
        // {
        //     string host = "localhost";
        //     string port =  "1433";
        //     string userid ="SA";
        //     string password = "Your_password1";
        //     string database = "teamfu";
        //     return $"Data Source={host},{port};Database={database};User Id={userid};Password={password};";
        // }

        
    }
}

