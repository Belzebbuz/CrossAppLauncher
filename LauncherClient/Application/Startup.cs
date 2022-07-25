using LauncherClient.ApplicationLayer.HttpServices;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherClient.ApplicationLayer
{
	public static class Startup
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			services.AddRefitClient<IProjectData>().ConfigureHttpClient(client =>
			{
				client.BaseAddress = new Uri("https://localhost:7187/api");//new Uri("https://a8986-e203.s.d-f.pw/api");
			});
			return services;
		}
	}
}
