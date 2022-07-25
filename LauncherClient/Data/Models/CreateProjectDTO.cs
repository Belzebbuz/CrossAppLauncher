using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherClient.Data.Models
{
    public class CreateProjectDTO
    {
		public CreateProjectDTO(string name, string description, bool isWinService, string exeFile)
		{
			Name = name;
			Description = description;
			IsWinService = isWinService;
			ExeFile = exeFile;
		}

		public string Name { get; }
		public string Description { get; }
		public bool IsWinService { get; }
		public string ExeFile { get; }
	}
}
