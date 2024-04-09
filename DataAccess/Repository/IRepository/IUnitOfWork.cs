namespace GroupManagement.DataAccess.Repository.IRepository
{
	public interface IUnitOfWork
	{
		IApplicationUserRepository ApplicationUser { get; }
		
		void Save();
		void SaveAsync();
	}
}
