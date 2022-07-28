﻿using System.ComponentModel.DataAnnotations.Schema;

namespace LauncherClient.Data.Models
{
	public class ProjectDTO : BaseEntity
	{
		public Guid? Id { get; set; }
		public string Name { get; set; }
		public string Author { get; set; }
		public string Description { get; set; }
		public bool IsWinService { get; set; }
		public string ExeFile { get; set; }
		public string CurrentVersion { get; set; }
		public List<ReleaseAssemblyDTO> ReleaseAssemblies { get; set; }
	}
}
