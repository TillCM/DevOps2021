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


        public static string GenerateDBConnectionFromEnv(Logger logger)
        {
            string host = GetEnvironmentVariableValue("DATABASE_SERVER", null, logger);
            string port = GetEnvironmentVariableValue("DATABASE_PORT", "5432", logger);
            string userid = GetEnvironmentVariableValue("DATABASE_USER", null, logger);
            string password = GetEnvironmentVariableValue("DATABASE_PASSWORD", null, logger);
            string database = GetEnvironmentVariableValue("DATABASE_NAME", null, logger);
            return $"Data Source={host},{port};Database={database};User Id={userid};Password={password};" ;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.AddDbContext<teamfuContext>(options=> options.UseSqlServer(this.Configuration.GetConnectionString(GenerateDBConnectionFromEnv(logger))));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "team_reece", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseEnvironnmentVariables();
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
}
