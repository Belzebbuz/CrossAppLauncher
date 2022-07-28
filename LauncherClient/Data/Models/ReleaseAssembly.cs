using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherClient.Data.Models
{
	public class ReleaseAssemblyDTO : BaseEntity
	{
		public Guid Id { get; set; }
		public string PatchNote { get; set; }
		public string Version { get; set; }
	}
}
