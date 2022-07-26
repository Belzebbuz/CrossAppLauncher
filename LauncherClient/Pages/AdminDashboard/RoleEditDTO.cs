using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherClient.Pages.AdminDashboard
{
    public class RoleEditDTO
    {
		public string UserId { get; set; }
		public List<SelectItemEntity> SelectedRoles { get; set; }
	}
}
