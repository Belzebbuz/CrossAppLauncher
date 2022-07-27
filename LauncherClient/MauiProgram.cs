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
using LauncherClient.Application;
using LauncherClient.Application.BackgroundServices;

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


#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
#endif
#if WINDOWS
		builder.Services.AddTransient<IFolderPicker, Platforms.Windows.FolderPicker>();
#endif
		builder.Services.AddSingleton<ExternalAuthStateProvider>(); 
		builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<ExternalAuthStateProvider>());
		builder.Services.AddAuthorizationCore();
		builder.Services.AddSingleton<AuthenticatedUser>();
		builder.Services.AddSingleton<IBackgroundService, GwtBackgroundService>();
		var host = builder.Build();
		var _ = host.Services.GetRequiredService<IBackgroundService>().ExecuteAsync();
		return host;
	}
}
