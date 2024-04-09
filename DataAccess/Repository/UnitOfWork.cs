using GroupManagement.DataAccess.Data;
using GroupManagement.DataAccess.Repository.IRepository;
using GroupManagement.Models;

namespace GroupManagement.DataAccess.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		public ApplicationDbContext _db;
		public IApplicationUserRepository ApplicationUser { get; private set; }


		public UnitOfWork(ApplicationDbContext db)
		{
			_db = db;
			ApplicationUser = new ApplicationUserRepository(_db);
		
		}

		public void Save()
		{
			_db.SaveChanges();
		}

		public void SaveAsync()
		{
			_db.SaveChangesAsync();
		}
	}
}
