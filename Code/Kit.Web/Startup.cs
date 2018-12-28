using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kit.Data.DatabaseLogic;
using Kit.Data.DatabaseLogic.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kit.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        #region ConfigureServices()
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApiBehaviorOptions(t =>
                {
                    t.SuppressModelStateInvalidFilter = true;         
                });
            #endregion

            #region DI
            services.AddTransient<UserService>();
            #endregion

            #region Database
            services.AddDbContext<DatabaseContext>(t =>
            {
                t.UseSqlServer(Configuration.GetConnectionString("KitDatabase"), s =>
                {  
                    s.MigrationsHistoryTable("Migrations", "Main");                    
                });
            });
            #endregion

            #region Swagger
            services.AddSwaggerGen(t =>
            {
                t.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Kit Api Doc",
                    Version = "v1"
                });
            });
            #endregion
        }
        #endregion

        #region Configure()
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();

            //app.UseHttpsRedirection();
            app.UseMvc();
          
            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(t =>
            {   
                t.SwaggerEndpoint("/kit/swagger/v1/swagger.json", "v1");
            });
            #endregion
         
        }
        #endregion
    }
}
