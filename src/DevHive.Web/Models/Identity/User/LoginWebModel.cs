using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using DevHive.Web.Attributes;

namespace DevHive.Web.Models.Identity.User
{
	public class LoginWebModel
	{
		[NotNull]
		[Required]
		[MinLength(3)]
		[MaxLength(50)]
		[OnlyAlphanumerics(ErrorMessage = "Username can only contain letters and digits!")]
		public string UserName { get; set; }

		[NotNull]
		[Required]
		[GoodPassword]
		public string Password { get; set; }
	}
}
