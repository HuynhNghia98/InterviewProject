﻿using System.ComponentModel.DataAnnotations;

namespace GroupManagement.Models.DTO.Auth
{
	public class LoginRequestDTO
	{
		[Required(ErrorMessage = "Nhập tài khoản")]
		public string Email { get; set; } = string.Empty;
		[Required(ErrorMessage = "Nhập mật khẩu")]
		public string Password { get; set; } = string.Empty;

	}
}
