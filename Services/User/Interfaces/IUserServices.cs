using GroupManagement.Models.DTO.User;
using GroupManagement.Models.Response;

namespace GroupManagement.Services.User.Interfaces
{
	public interface IUserServices
	{
		public Task<ApiResponse<GetUsersResponseDTO>> GetUsersAsync();
		public Task<ApiResponse<Models.ApplicationUser>> GetUserAsync(string userId);
		public Task<ApiResponse<object>> CreateUserAsync(AddUserRequestDTO model);
		public Task<ApiResponse<object>> UpdateUserAsync(string userId, UpdateUserRequestDTO model);
		public Task<ApiResponse<object>> DeleteUserAsync(string userId);

	}
}
