using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GroupManagement.DataAccess.Data;
using GroupManagement.DataAccess.Repository.IRepository;
using GroupManagement.Models;
using GroupManagement.Utilities;

namespace GroupManagement.DataAccess.DbInitializer
{
	public class DbInitializer : IDbInitializer
	{
		private readonly ApplicationDbContext _db;
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public DbInitializer(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork)
		{
			_db = db;
			_userManager = userManager;
			_roleManager = roleManager;
			_unitOfWork = unitOfWork;
		}

		public void Initializer()
		{
			try
			{
				if (_db.Database.GetPendingMigrations().Count() > 0)
				{
					_db.Database.Migrate();
				}
			}
			catch (Exception ex) { }

			//Tạo tài khoản admin
			var newUser = new ApplicationUser
			{
				UserName = "admin",
				Email = "nghiaht0412@gmail.com",
				Name = "Nghia",
				PhoneNumber = "0123456789",
			};

			_userManager.CreateAsync(newUser, "123").GetAwaiter().GetResult();

			var user = _db.ApplicationUsers.FirstOrDefault(x => x.Id == newUser.Id);
		}
	}
}
