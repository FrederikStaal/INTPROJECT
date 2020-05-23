using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CardAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CardApi
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
            services.AddControllers();
            // Adding GameContext to the container
            services.AddDbContext<GameContext>(options => options.UseSqlServer(Configuration.GetConnectionString("GameDb")));

            // Declaring Cors Policy - Allow access to all sources
            //services.AddCors(o => o.AddPolicy("GamePolicy", builder =>
            //{
            //    builder.AllowAnyOrigin() // Request can come from any domain
            //    .AllowAnyMethod() // All methods can be used - GET, PUT etc.
            //    .AllowAnyHeader(); // All headers are allowed by the HTTP protocol
            //}));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }




            // Using Dummy Data
            DummyData.Initialize(app);

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
