using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DevHive.Web.Configurations.Extensions;
using Newtonsoft.Json;
using Serilog;

namespace DevHive.Web
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
			services.AddCors();

			services.AddControllers()
				.AddNewtonsoftJson(x =>
				{
					x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
				});

			services.DependencyInjectionConfiguration(this.Configuration);
			services.DatabaseConfiguration(Configuration);
			services.SwaggerConfiguration();
			services.JWTConfiguration(Configuration);
			services.AutoMapperConfiguration();
			services.ConfigureExceptionHandler(this.Configuration);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseCors(x => x
				.AllowAnyMethod()
				.AllowAnyHeader()
				.SetIsOriginAllowed(origin => true) // allow any origin
				.AllowCredentials()); // allow credentials

			if (env.IsDevelopment())
			{
				app.UseExceptionHandlerMiddlewareConfiguration();
				// app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseHsts();
				app.UseExceptionHandlerMiddlewareConfiguration();
			}

			app.UseSwaggerConfiguration();
			app.UseDatabaseConfiguration();
			app.UseAutoMapperConfiguration();

			app.UseSerilogRequestLogging();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "api/{controller}/{action}"
				);
			});
		}
	}
}
