using System.Text;
using System.Threading.Tasks;
using Data.Models.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
	public static class JWTExtensions
	{
		public static void JWTConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton(new JWTOptions(configuration
						.GetSection("AppSettings")
						.GetSection("Secret")
						.Value));

			// Get key from appsettings.json
			var key = Encoding.ASCII.GetBytes(configuration
						.GetSection("AppSettings")
						.GetSection("Secret")
						.Value);

			// Setup Jwt Authentication
			services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.Events = new JwtBearerEvents
				{
					OnTokenValidated = context =>
					{
						// TODO: add more authentication
						return Task.CompletedTask;
					}
				};
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});
		}
	}
}