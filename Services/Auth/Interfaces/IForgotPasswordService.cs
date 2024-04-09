using GroupManagement.Models.DTO.Auth;
using GroupManagement.Models.Response;

namespace GroupManagement.Services.Auth.Interfaces
{
    public interface IForgotPasswordService
    {
        Task<ApiResponse<object>> ForgotPassword(ForgotPasswordRequestDTO model);
        Task<ApiResponse<object>> ChangePassword(ChangePasswordRequestDTO model);
    }
}
