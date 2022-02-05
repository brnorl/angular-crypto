using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using back.Infrastructure;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Hangfire.MemoryStorage;
using back.Currency;

namespace back
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
            var _mappingProfile = new MapperConfiguration(mp => { mp.AddProfile(new MappingProfiles()); });
            IMapper mapper = _mappingProfile.CreateMapper();
            services.AddSingleton(mapper);

            services.AddHangfire(config =>
                config.SetDataCompatibilityLevel(compatibilityLevel: CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseMemoryStorage());

            services.AddHangfireServer();

            services.AddTransient<ICurrencyUpdate, CurrencyUpdate>();
            services.AddCors();

            services.AddControllers();
            services.AddHttpClient();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "back", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "back v1"));

                
            }
            app.UseCors(builder => builder
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowAnyMethod()
            );
            app.UseHangfireDashboard();

            app.UseHttpsRedirection();

            RecurringJob.AddOrUpdate(
            "Update BTC",
            () => serviceProvider.GetService<ICurrencyUpdate>().UpdateCurrency(),
            Cron.Minutely);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
