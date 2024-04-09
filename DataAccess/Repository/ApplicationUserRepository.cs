using GroupManagement.DataAccess.Data;
using GroupManagement.DataAccess.Repository.IRepository;
using GroupManagement.Models;

namespace GroupManagement.DataAccess.Repository
{
	public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
	{
		public ApplicationUserRepository(ApplicationDbContext db) : base(db)
		{
		}

		public void Update(ApplicationUser ApplicationUser)
		{
			_db.Update(ApplicationUser);
		}
	}
}
