using System.ComponentModel.DataAnnotations;

namespace GroupManagement.Models.DTO.Auth
{
	public class ForgotPasswordRequestDTO
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
	}
}
