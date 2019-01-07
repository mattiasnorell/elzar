using Autofac;
using System;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Feedbag.Pdf;
using Feedbag.Business.Scraper;
using Feedbag.Business.Parser;
using Feedbag.Business.Mappers;
using Feedbag.DataAccess.Providers;
using Feedbag.DataAccess.Repositories;
using Feedbag.Business.Providers;
using Microsoft.AspNetCore.HttpOverrides;
using Feedbag.Models;

namespace Feedbag
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            Microsoft.Extensions.DependencyInjection.OptionsConfigurationServiceCollectionExtensions.Configure<ConnectionStrings>(services, Configuration.GetSection("ConnectionStrings"));

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
            builder.RegisterType<IngredientRepository>().As<IIngredientRepository>();
            builder.RegisterType<CookingProcedureRepository>().As<ICookingProcedureRepository>();
            builder.RegisterType<RecipeRepository>().As<IRecipeRepository>();
            var ApplicationContainer = builder.Build();
            
            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
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

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
