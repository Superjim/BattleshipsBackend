using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace BattleshipsBackend
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Battleships API",
                    Version = "v1",
                    Description = "API for Battleships game",
                    Contact = new OpenApiContact
                    {
                        Name = "Jim Jenkinson",
                        Email = "jamesjenkinson10@gmail.com"
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app)
        {

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Battleships API v1");
            });
        }
    }
}
