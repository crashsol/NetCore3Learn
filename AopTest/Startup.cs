using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AopTest.Data;
using AopTest.Interceptors;
using AopTest.Services;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AopTest
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite("Data Source =app.db");
            });

            //添加服务方法
            services.AddScoped<IUserService, UserService>();

            //服务拦截器
            services.AddTransient<CustomInterceptorAttribute>();

            //添加全局拦截器
            services.ConfigureDynamicProxy(config =>
            {
                //配置全局拦截器,可以拦截由serviceProvider创建的任何服务的方法。
                //config.Interceptors.AddTyped<CustomInterceptorAttribute>();
            });        

            //services.WeaveDynamicProxyService();

            return services.BuildDynamicProxyServiceProvider();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
