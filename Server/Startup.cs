using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RedisCache;

namespace DragDropExample.Server
{
    public class Startup
    {
        public IConfiguration Config { get; }
        public IWebHostEnvironment Env { get; }

        public Startup(
            IConfiguration Config,
            IWebHostEnvironment Env)
        {
            this.Env = Env;
            this.Config = Config;
        }        

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddDragDropTaskManager<TaskManager>();

            services.AddAzureRedisCache(o => 
            {
                var redisSection = Config.GetSection("Cache");

                o.Host = redisSection.GetValue<string>("Host");
                o.SSLPort = redisSection.GetValue<string>("SSLPort");
                o.Settings = redisSection.GetValue<CacheConfiguration[]>("Settings");
            });

            services.AddAzureStorageServices(o =>
            {
                o.IsDevelopment = true;
            });

            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", opt => opt
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
