using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace LauncherClient.Utilities;

public class ExternalAuthStateProvider : AuthenticationStateProvider
{
	private readonly object _locker = new();
	public List<string> Roles { get; } = new List<string> { "Admin", "Dev", "Viewer" };
	public AuthenticationState AuthenticationState { get; private set; }
	public async Task Login(string token)
	{
		await SecureStorage.SetAsync("token", token);

		NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
	}

	public async Task Logout()
	{
		SecureStorage.Remove("token");
		NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
	}

	public override async Task<AuthenticationState> GetAuthenticationStateAsync()
	{
		try
		{
			var token = await SecureStorage.GetAsync("token");
			if (token != null)
			{
				var claims = ParseClaimsFromJwt(token).ToList();
				var identity = new ClaimsIdentity(claims, "jwt");
				AuthenticationState = new AuthenticationState(new ClaimsPrincipal(identity));
				NotifyAuthenticationStateChanged(Task.FromResult(AuthenticationState));
				return AuthenticationState;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Request failed:" + ex.ToString());
		}
		return new AuthenticationState(new ClaimsPrincipal());
	}
	public static async Task<string> GetTokenAsync()
	{
		var token = await SecureStorage.GetAsync("token");
		return token;
	}
	public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
	{
		var claims = new List<Claim>();
		var payload = jwt.Split('.')[1];
		var jsonBytes = ParseBase64WithoutPadding(payload);
		var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
		ExtractRolesFromJWT(claims, keyValuePairs);
		claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));
		return claims;
	}
	private static void ExtractRolesFromJWT(List<Claim> claims, Dictionary<string, object> keyValuePairs)
	{
		keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);
		if (roles != null)
		{
			var parsedRoles = roles.ToString().Trim().TrimStart('[').TrimEnd(']').Split(',');
			if (parsedRoles.Length > 1)
			{
				foreach (var parsedRole in parsedRoles)
				{
					claims.Add(new Claim(ClaimTypes.Role, parsedRole.Trim('"')));
				}
			}
			else
			{
				claims.Add(new Claim(ClaimTypes.Role, parsedRoles[0]));
			}
			keyValuePairs.Remove(ClaimTypes.Role);
		}
	}

	private static byte[] ParseBase64WithoutPadding(string base64)
	{
		switch (base64.Length % 4)
		{
			case 2: base64 += "=="; break;
			case 3: base64 += "="; break;
		}
		return Convert.FromBase64String(base64);
	}
}
public class AuthenticatedUser
{
	public ClaimsPrincipal Principal { get; set; } = new();
}