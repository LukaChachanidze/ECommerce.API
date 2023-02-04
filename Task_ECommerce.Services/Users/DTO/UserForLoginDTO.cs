namespace Task_ECommerce.Services.Users.DTO
{
    /// <summary>
    /// Data transfer object to login user
    /// </summary>
    public class UserForLoginDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
