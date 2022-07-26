using LauncherClient.Data.Auth;
using LauncherClient.Data.Models;
using LauncherClient.Pages.AdminDashboard;
using Refit;

namespace LauncherClient.Application.HttpServices
{
	public interface IAccountsClient
    {
        [Post("/create")]
        Task<AuthenticationResponse> RegisterAsync(UserCredentials userCredentials);
		[Post("/login")]
		Task<AuthenticationResponse> LoginAsync(UserCredentials userCredentials);

        [Get("/listUsers")]
		[Headers("Authorization: Bearer")]
		Task<List<UserDTO>> GetAllUsersAsync();

		[Get("/delete/{id}")]
		[Headers("Authorization: Bearer")]
		Task DeleteUserAsync(string id);

		[Post("/editRoles")]
		[Headers("Authorization: Bearer")]
		Task EditRolesAsync(RoleEditDTO dto);
	}
}
