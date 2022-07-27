using LauncherClient.Application.HttpServices;
using LauncherClient.ApplicationLayer.HttpServices;
using LauncherClient.Utilities;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Refit;
using System.Net.Http.Headers;

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
			client.BaseAddress = new Uri("https://localhost:7187/api");//new Uri("https://a8986-e203.s.d-f.pw/api");
		});

		services.AddRefitClient<IAccountsClient>(new RefitSettings()
		{
			AuthorizationHeaderValueGetter = () => ExternalAuthStateProvider.GetTokenAsync()
		}).ConfigureHttpClient(client =>
		{
			client.BaseAddress = new Uri("https://localhost:7187/accounts");//new Uri("https://a8986-e203.s.d-f.pw/api");
		});

		return services;
	}

	
}
