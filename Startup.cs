using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ReadingIsGood.DomainInterfaces;
using ReadingIsGood.DomainServices;
using ReadingIsGood.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingIsGood
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
            services.AddDbContext<ReadingIsGoodDbContext>(opt => 
            {
                // IIS Express ve Internal MSSQL EXPRESS veritabaný baðlantý cümlesi
                //opt.UseSqlServer("server=.;database=ReadingIsGood;User Id=sa;Password=abc123,Persist Security Info=true;Trusted_Connection=True;");
                
                // Docker-LinuxUbuntu ve MSSQL Express baðlantý cümlesi
                opt.UseSqlServer("Server=192.168.80.1,1401;Database=ReadingIsGood;User Id=sa;Password=1q2w3e4r5t6y!");
            });
            services.AddControllers();
            services.AddTransient<IReadingIsGoodRepository, ReadingIsGoodRepository>();
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ReadingIsGood", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ReadingIsGood v1"));
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
