using LauncherClient.Application.HttpServices;
using LauncherClient.Utilities;
using Microsoft.Extensions.Hosting;

namespace LauncherClient.Application.BackgroundServices
{
    public class GwtBackgroundService : IBackgroundService
    {
		private readonly IServiceProvider _serviceProvider;
		public GwtBackgroundService(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public async Task ExecuteAsync()
		{
			while (true)
			{
				using var scope = _serviceProvider.CreateScope();
				var client = scope.ServiceProvider.GetRequiredService<IAccountsClient>();
				var authProvider = scope.ServiceProvider.GetRequiredService<ExternalAuthStateProvider>();
				var currentToken = await SecureStorage.GetAsync("token");
				var currentCountClaims = currentToken == null ? 0 : ExternalAuthStateProvider.ParseClaimsFromJwt(currentToken).Count();
				try
				{
					var credit = await client.GetJwtAsync();
					if (credit != null)
					{
						var newCountClaims = ExternalAuthStateProvider.ParseClaimsFromJwt(credit.Token).Count();
						if (currentCountClaims != newCountClaims)
							await authProvider.Login(credit.Token);
					}
					else
					{
						await authProvider.Logout();
					}
				}
				catch (Exception ex)
				{
					await authProvider.Logout();
				}

				await Task.Delay(30000);
			}
		}
	}
}
