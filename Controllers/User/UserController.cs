using GroupManagement.Models.DTO.User;
using GroupManagement.Services.User.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GroupManagement.Controllers.User
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IUserServices _user;

		public UserController(IUserServices user)
		{
			_user = user;
		}

		[HttpGet("GetUsers")]
		public async Task<IActionResult> GetUsers()
		{
			var result = await _user.GetUsersAsync();

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpGet("GetUser/{id}")]
		public async Task<IActionResult> GetUser(string id)
		{
			var result = await _user.GetUserAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPost("AddUser")]
		public async Task<IActionResult> AddUser([FromForm] AddUserRequestDTO model)
		{
			var result = await _user.CreateUserAsync(model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpPut("UpdateUser/{id}")]
		public async Task<IActionResult> UpdateUser(string id, [FromForm] UpdateUserRequestDTO model)
		{
			var result = await _user.UpdateUserAsync(id, model);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpDelete("DeleteUser/{id}")]
		public async Task<IActionResult> DeleteUser(string id)
		{
			var result = await _user.DeleteUserAsync(id);

			if (result.IsSuccess)
			{
				return Ok(result);
			}
			else
			{
				return BadRequest(result);
			}
		}
	}
}
