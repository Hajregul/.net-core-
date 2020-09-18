using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IB150232_API.Database;
using IB150232_API.Filters;
using IB150232_API.Services;
using IB150232_API.Security;
using IB150232_Model.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;

namespace IB150232_API
{

    public class BasicAuthDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            var securityRequirements = new Dictionary<string, IEnumerable<string>>()
        {
            { "basic", new string[] { } }  // in swagger you specify empty list unless using OAuth2 scopes
        };

            swaggerDoc.Security = new[] { securityRequirements };
        }
    }
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
            services.AddMvc(x => x.Filters.Add<ErrorFilter>()).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAutoMapper();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.AddSecurityDefinition("basic", new BasicAuthScheme() { Type = "basic" });
                c.DocumentFilter<BasicAuthDocumentFilter>();
            });


            services.AddAuthentication("BasicAuthentication")
             .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

        
            services.AddScoped<IKorisniciService, KorisniciService>();
            services.AddScoped<IService<IB150232_Model.KorisniciUloge, object>, BaseService<IB150232_Model.KorisniciUloge, object, KorisniciUloge>>();
            services.AddScoped<IService<IB150232_Model.Uloge, object>, BaseService<IB150232_Model.Uloge, object, Uloge>>();
           // services.AddScoped<IService<IB150232_Model.KorisniciUloge, object>, BaseService<IB150232_Model.KorisniciUloge, object, KorisniciUloge>>();

          //  services.AddScoped<ICRUDService<Model.Proizvod, ProizvodSearchRequest, ProizvodUpsertRequest, ProizvodUpsertRequest>, ProizvodService>();

            var connection = @"Server=DESKTOP-S0I4096\SQLEXPRESS;Database=IB150232;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<IB150232Context>(options => options.UseSqlServer(connection));

            
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseAuthentication();
           // app.UseHttpsRedirection();
            app.UseMvc();


        }
    }
}
