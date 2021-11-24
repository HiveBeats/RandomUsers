using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApi.Configuration;
using WebApi.Middlewares;

namespace WebApi
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
            
            var randomUserServiceSection = Configuration.GetSection(nameof(RandomUserServiceConfig));
            services.Configure<RandomUserServiceConfig>(randomUserServiceSection);

            services.AddHttpContextAccessor();

            InjectAllFeatures(services);

            services.AddHealthChecks();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Backend",
                    Version = "v1",
                    Contact = new OpenApiContact()
                    {
                        Name = "Ivan Laletin",
                        Url = new Uri("https://e1lama.ru")
                    }
                });
            });
            
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });

            //services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else 
            {
                
            }

            app.UseHttpsRedirection();
            app.UseHealthChecks("/health_check");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My API V1");
            });
            
            app.UseRouting();

            app.UseCors("AllowAll");
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        public void InjectAllFeatures(IServiceCollection services)
        {
            var featureInjectors = (from domainAssembly in AppDomain.CurrentDomain.GetAssemblies()
                                    from assemblyType in domainAssembly.GetExportedTypes()
                                    where assemblyType.IsSubclassOf(typeof(InjectorBase))
                                    select assemblyType).ToArray();
            
            foreach (var i in featureInjectors)
            {
                var instance = Activator.CreateInstance(i);
                (instance as InjectorBase)?.Inject(services);
            }
        }
    }
}