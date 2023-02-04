using System.ComponentModel.DataAnnotations;

namespace Task_ECommerce.Services.Users.DTO
{
    /// <summary>
    /// Data transfer object to Register new user
    /// </summary>
    public class UserForRegistrationDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        /// <summary>
        /// Ensuring correct email format with attribute 
        /// </summary>
        [EmailAddress(ErrorMessage = "Incorrect Email Format")]
        public string Email { get; set; }
    }
}
