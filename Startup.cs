﻿using Autofac;
using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Elzar.Pdf;
using Elzar.Business.Scraper;
using Elzar.Business.Parser;
using Elzar.Business.Mappers;
using Elzar.DataAccess.Providers;
using Elzar.DataAccess.Repositories;
using Elzar.Business.Providers;
using Microsoft.AspNetCore.HttpOverrides;
using Elzar.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Elzar.FileHandlers;

namespace Elzar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public CorsPolicy GenerateCorsPolicy(){
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); // For anyone access.
           // corsBuilder.WithOrigins("http://localhost:8080/"); // for a specific url. Don't add a forward slash on the end!
            corsBuilder.AllowCredentials();
            return corsBuilder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(options =>
                {
                    options.AddPolicy("AllowAllOrigins", GenerateCorsPolicy());
                });

            Microsoft.Extensions.DependencyInjection.OptionsConfigurationServiceCollectionExtensions.Configure<ConnectionStrings>(services, Configuration.GetSection("ConnectionStrings"));
            Microsoft.Extensions.DependencyInjection.OptionsConfigurationServiceCollectionExtensions.Configure<AppSettings>(services, Configuration.GetSection("AppSettings"));

            var builder = new ContainerBuilder();
            builder.Populate(services);  
            builder.RegisterType<SelectPdfGenerator>().As<IPdfGenerator>();   
            builder.RegisterType<Scraper>().As<IScraper>(); 
            builder.RegisterType<RecipeParser>().As<IRecipeParser>();
            builder.RegisterType<IngredientParser>().As<IIngredientParser>();
            builder.RegisterType<SiteSettingsProvider>().As<ISiteSettingsProvider>();
            
            builder.RegisterType<IngredientProvider>().As<IIngredientProvider>();
            builder.RegisterType<CookingProcedureProvider>().As<ICookingProcedureProvider>();
            builder.RegisterType<RecipeProvider>().As<IRecipeProvider>();
            
            builder.RegisterType<IngredientMapper>().As<IIngredientMapper>();
            builder.RegisterType<CookingProcedureMapper>().As<ICookingProcedureMapper>();
            builder.RegisterType<RecipeMapper>().As<IRecipeMapper>();

            // Dataaccess
            builder.RegisterType<MySqlConnectionProvider>().As<IDbConnectionProvider>();

            builder.RegisterType<IngredientRepository>().As<IIngredientRepository>();
            builder.RegisterType<CookingProcedureRepository>().As<ICookingProcedureRepository>();
            builder.RegisterType<RecipeRepository>().As<IRecipeRepository>();

            builder.RegisterType<FileHandler>().As<IFileHandler>();

            var ApplicationContainer = builder.Build();
            
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.UseCors("AllowAllOrigins");
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
