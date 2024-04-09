using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupManagement.Models
{
	public class ApplicationUser : IdentityUser
	{
		[Required]
		public string Name { get; set; } = string.Empty;
		public byte[]? Avatar { get; set; }
		public string? Extension { get; set; } 
		public DateTime? Birthday { get; set; }
		public int? Gender { get; set; } // 0 là nam, 1 là nữ
		public DateTime CreatedAt { get; set; } = DateTime.Now;
	}
}
