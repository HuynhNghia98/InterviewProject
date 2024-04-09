using GroupManagement.DataAccess.Data;
using GroupManagement.DataAccess.Repository.IRepository;
using GroupManagement.Models;
using GroupManagement.Models.DTO.User;
using GroupManagement.Models.Response;
using GroupManagement.Services.User.Interfaces;
using GroupManagement.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Xml.Linq;

namespace GroupManagement.Services.User
{
	public class UserServices : IUserServices
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly ApplicationDbContext _db;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private ApiResponse<object> _res;
		public UserServices(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext db)
		{
			_unitOfWork = unitOfWork;
			_roleManager = roleManager;
			_userManager = userManager;
			_db = db;
			_res = new();
		}

		public async Task<ApiResponse<GetUsersResponseDTO>> GetUsersAsync()
		{
			ApiResponse<GetUsersResponseDTO> res = new();

			var usersInDb = await _unitOfWork.ApplicationUser
				.GetAll()
				.ToListAsync();

			res.Result.Users = usersInDb;
			return res;
		}

		public async Task<ApiResponse<ApplicationUser>> GetUserAsync(string userId)
		{
			ApiResponse<ApplicationUser> res = new();

			var userInDb = await _unitOfWork.ApplicationUser
				.Get(x => x.Id.Equals(userId), true)
				.FirstOrDefaultAsync();

			if (userInDb == null)
			{
				res.Errors = new Dictionary<string, List<string>>
							{
								{ "userId", new List<string> { $"Không tìm thấy người dùng" }}
							};
				res.IsSuccess = false;
				return res;
			}

			res.Result = userInDb;
			return res;
		}

		public async Task<ApiResponse<object>> CreateUserAsync(AddUserRequestDTO model)
		{
			var userInDbWithEmail = await _unitOfWork.ApplicationUser.Get(x => x.Email.Equals(model.Email), true).FirstOrDefaultAsync();

			//kiểm tra trùng username
			if (userInDbWithEmail != null)
			{
				_res.Errors = new Dictionary<string, List<string>>
							{
								{ nameof(model.Email), new List<string> { $"Trùng email" }}
							};
				_res.IsSuccess = false;
				return _res;
			}

			var newUser = new ApplicationUser();
				
			if (model.File != null && model.File.Length > 0)
			{
				string extension = Path.GetExtension(model.File.FileName);
				byte[] imageData;
				using (var memoryStream = new MemoryStream())
				{
					await model.File.CopyToAsync(memoryStream);
					imageData = memoryStream.ToArray();
				}
				newUser.Avatar = imageData;
				newUser.Extension = extension;

				newUser.Name = model.Name;
				newUser.Email = model.Email;
				newUser.PhoneNumber = model.Phone;
				newUser.Birthday = model.Birthday;
				newUser.Gender = model.Gender;
				newUser.UserName = model.Email;
			}
			else
			{
				newUser.Name = model.Name;
				newUser.Email = model.Email;
				newUser.PhoneNumber = model.Phone;
				newUser.Birthday = model.Birthday;
				newUser.Gender = model.Gender;
				newUser.UserName = model.Email;
			}

			var result = await _userManager.CreateAsync(newUser, model.Password);

			if (!result.Succeeded)
			{
				_res.Errors = new Dictionary<string, List<string>>
							{
								{ "error", new List<string> { $"Không thể tạo mới" }}
							};
				_res.IsSuccess = false;
				return _res;
			}

			_res.Messages = "Tạo mới người dùng thành công";
			return _res;
		}

		public async Task<ApiResponse<object>> UpdateUserAsync(string userId, UpdateUserRequestDTO model)
		{
			if (string.IsNullOrEmpty(userId))
			{
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "userId", new List<string> { $"Không có id người dùng" }}
					};
				_res.IsSuccess = false;
				return _res;
			}

			var userInDb = await _unitOfWork.ApplicationUser.Get(x => x.Id.Equals(userId), true).FirstOrDefaultAsync();
			if (userInDb == null)
			{
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "userId", new List<string> { $"Không tìm thấy người dùng" }}
					};
				_res.IsSuccess = false;
				return _res;
			}

			var userInDbToCheckEmail = await _unitOfWork.ApplicationUser
				.Get(x => (x.Email.Equals(model.Email)) && x.Id != userInDb.Id, true)
				.FirstOrDefaultAsync();

			//kiểm tra trùng email
			if (userInDbToCheckEmail != null && userInDbToCheckEmail.Id != userInDb.Id)
			{
				_res.Errors = new Dictionary<string, List<string>>
							{
								{ "error", new List<string> { $"Email đã tồn tại" }}
							};
				_res.IsSuccess = false;
				return _res;
			}

			if (model.File != null && model.File.Length > 0)
			{
				string extension = Path.GetExtension(model.File.FileName);
				byte[] imageData;
				using (var memoryStream = new MemoryStream())
				{
					await model.File.CopyToAsync(memoryStream);
					imageData = memoryStream.ToArray();
				}
				userInDb.Avatar = imageData;
				userInDb.Extension = extension;

				userInDb.Name = model.Name;
				userInDb.Email = model.Email;
				userInDb.PhoneNumber = model.Phone;
				userInDb.Birthday = model.Birthday;
				userInDb.Gender = model.Gender;
				userInDb.UserName = model.Email;
			}
			else
			{
				userInDb.Name = model.Name;
				userInDb.Email = model.Email;
				userInDb.PhoneNumber = model.Phone;
				userInDb.Birthday = model.Birthday;
				userInDb.Gender = model.Gender;
				userInDb.UserName = model.Email;
			}

			_unitOfWork.ApplicationUser.Update(userInDb);
			_unitOfWork.Save();

			_res.Messages = "Cập nhật người dùng thành công";
			return _res;
		}

		public async Task<ApiResponse<object>> DeleteUserAsync(string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				_res.Errors = new Dictionary<string, List<string>>
					{
						{ "userId", new List<string> { $"Không có id người dùng" }}
					};
				_res.IsSuccess = false;
				return _res;
			}

			var userInDb = await _unitOfWork.ApplicationUser
				.Get(x => x.Id == userId, true)
				.FirstOrDefaultAsync();

			if (userInDb == null)
			{
				_res.Errors = new Dictionary<string, List<string>>
				{
					{ "userId", new List<string> { $"Không tìm thấy người dùng" }}
				};
				_res.IsSuccess = false;
				return _res;
			}

			_unitOfWork.ApplicationUser.Remove(userInDb);
			_unitOfWork.Save();

			_res.Messages = "Đã xóa người dùng thành công";
			return _res;
		}

	}
}
