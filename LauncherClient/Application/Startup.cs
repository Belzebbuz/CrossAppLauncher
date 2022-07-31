using LauncherClient.Application.HttpServices;
using LauncherClient.ApplicationLayer.HttpServices;
using LauncherClient.Utilities;
using Refit;

namespace LauncherClient.ApplicationLayer;

public static class Startup
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddRefitClient<IProjectData>(new RefitSettings()
		{
			AuthorizationHeaderValueGetter = () => ExternalAuthStateProvider.GetTokenAsync()
		}).ConfigureHttpClient(client =>
		{
			client.BaseAddress = new Uri("http://192.168.0.126:5122/api");//new Uri("https://a8986-e203.s.d-f.pw/api");
		});

		services.AddRefitClient<IAccountsClient>(new RefitSettings()
		{
			AuthorizationHeaderValueGetter = () => ExternalAuthStateProvider.GetTokenAsync()
		}).ConfigureHttpClient(client =>
		{
			client.BaseAddress = new Uri("http://192.168.0.126:5122/accounts");//new Uri("https://a8986-e203.s.d-f.pw/api");
		});

		return services;
	}

	
}
