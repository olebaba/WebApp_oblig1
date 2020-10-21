using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using oblig1_1.DAL;
using oblig1_1.Models;

namespace oblig1_1
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
            Log.Logger = new LoggerConfiguration()
                     .MinimumLevel.Debug()
            // Logger Information, Warning, Error og Fatal i "info-logs.txt"-fil
            .WriteTo.File(path: @"Logs/info-log.txt", restrictedToMinimumLevel: LogEventLevel.Information, rollingInterval: RollingInterval.Day)
            // Logger Error og Fatal i "error-logs.txt"-fil
            .WriteTo.File(path: @"Logs/error-log.txt", restrictedToMinimumLevel: LogEventLevel.Error, rollingInterval: RollingInterval.Day)
            .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddDbContext<BestillingContext>(options => 
                        options.UseSqlite("Data Source=Bestillinger.db"));
            services.AddScoped<IBestillingRepository, BestillingRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                DBInit.Init(app);
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseStaticFiles(); //Bruke HTML-filen

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
