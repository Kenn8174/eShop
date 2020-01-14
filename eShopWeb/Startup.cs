using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer;
using ServiceLayer.ShopService;

namespace eShopWeb
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //services.AddSingleton<IDateTime, SystemDateTime>();
            //services.AddTransient<IDateTime, SystemDateTime>();
            //services.AddScoped<IDateTime, SystemDateTime>();
                       
            services.AddScoped<IShopService, ShopService>();
            services.AddDbContext<ShopContext>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddDbContext<ShopContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("RazorPagesMovieContext")));

            //services.AddMiniProfiler(options =>
            //{
            //    options.PopupShowTimeWithChildren = true;
            //})
            //.AddEntityFramework();

            services.AddSession();
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                //app.UseStatusCodePagesWithRedirects("/Error");
                app.UseStatusCodePagesWithReExecute("/Error");
                //app.UseStatusCodePages();
                //app.UseDatabaseErrorPage();

                app.UseMiniProfiler();
            }
            else
            {
                //app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
           

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("hej 1!");

            //    await next();
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("den anden!");
            //});

            app.UseMvc();

        }
    }
}
