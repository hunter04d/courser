using System;
using System.IO;
using System.Text.Json.Serialization;
using Application.Abstractions;
using Application.Behaviors;
using FluentValidation.AspNetCore;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;
using Serilog;
using Swashbuckle.AspNetCore.Swagger;

namespace WebApp
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Database
            var connString = _configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<IAppDbContext, AppDbContext>(options =>
                options.UseNpgsql(connString)
            );

            // MediatR
            services.AddMediatR(typeof(Application.Application));
            services.AddTransient(typeof(IRequestPreProcessor<>), typeof(RequestLogger<>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidator<,>));

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.AddFluentValidationRules();
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Courser Api", Version = "v1"});

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "WebApp.xml"));
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Application.xml"));
            });

            // MVC
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                if (_env.IsDevelopment()) options.JsonSerializerOptions.WriteIndented = true;
            }).AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Application.Application>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();
            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Courser API"); });

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
