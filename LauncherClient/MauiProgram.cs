using Microsoft.AspNetCore.Components.WebView.Maui;
using LauncherClient.Data;
using MudBlazor.Services;
using LauncherClient.ApplicationLayer;
using Microsoft.Maui.Controls.PlatformConfiguration;
using LauncherClient.Utilities;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using Refit;
using LauncherClient.Application.HttpServices;

namespace LauncherClient;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddMauiBlazorWebView();
		builder.Services.AddMudServices();
		builder.Services.AddApplication();
		builder.Services.AddSingleton<IFilePicker>(FilePicker.Default);
		builder.Services.AddSingleton<WeatherForecastService>();


#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
#if WINDOWS
		builder.Services.AddTransient<IFolderPicker, Platforms.Windows.FolderPicker>();
#endif
		builder.Services.AddScoped<ExternalAuthStateProvider>(); // This is our custom provider
																 //When asking for the default Microsoft one, give ours!
		builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<ExternalAuthStateProvider>());
		builder.Services.AddAuthorizationCore();
		builder.Services.AddSingleton<AuthenticatedUser>();
		var host = builder.Build();
		return host;
	}
}
