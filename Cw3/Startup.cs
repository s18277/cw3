using Cw3.DAL;
using Cw3.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cw3
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
            services.AddTransient<IDbService<Student>, MockStudentDbService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            // Dodanie nowego middleware który dodaje do odpowiedzi nowy Header.
            // Jest utworzony in-line jako wyrażenie lambda.
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Custom-Header-1", "Custom-Header-Value-1");
                await next.Invoke();
            });

            // Dodanie nowego middleware który dodaje do odpowiedzi nowy Header.
            // Tym razem jest on zdefiniowany jako osobna klasa.
            app.UseMiddleware<CustomMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}