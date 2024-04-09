using GroupManagement.Models.DTO.Auth;
using GroupManagement.Models.Response;

namespace GroupManagement.Services.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<object>> Login(LoginRequestDTO loginRequestDTO);
    }
}
