using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LauncherClient.Data.Auth
{
    public class UserCredentials
    {
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[StringLength(30, ErrorMessage = "Пароль должен состоять не менее чем из 8 символов", MinimumLength = 8)]
		public string Password { get; set; }
	}
}
