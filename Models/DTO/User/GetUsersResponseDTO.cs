namespace GroupManagement.Models.DTO.User
{
	public class GetUsersResponseDTO
	{
        public GetUsersResponseDTO()
        {
			Users = new();

		}
        public List<Models.ApplicationUser> Users { get; set; }
    }
}
