using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherClient.Data.Auth
{
	public class UserDTO
	{
		public string Id { get; set; }
		public string Email { get; set; }
		public List<string> Roles { get; set; }
	}
}
