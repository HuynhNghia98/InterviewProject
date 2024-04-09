using System.ComponentModel.DataAnnotations;

namespace GroupManagement.Models.DTO.User
{
	public class AddUserRequestDTO
	{
		[Required]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
		[Required]
		public string Name { get; set; } = string.Empty;
		public string? Phone { get; set; } = string.Empty;
		public DateTime? Birthday { get; set; }
		public int? Gender { get; set; } // 0 là nam, 1 là nữ

		public IFormFile? File { get; set; }
		
	}
}
