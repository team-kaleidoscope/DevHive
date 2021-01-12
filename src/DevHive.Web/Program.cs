using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace DevHive.Web
{
	public class Program
	{
		private const int HTTP_PORT = 5000;

		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.ConfigureKestrel(opt => opt.ListenLocalhost(HTTP_PORT));
					webBuilder.UseStartup<Startup>();
				});
	}
}
