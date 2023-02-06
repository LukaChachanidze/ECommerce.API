using Microsoft.AspNetCore.Mvc;
using Task_ECommerce.Services.Users;
using Task_ECommerce.Services.Users.DTO;

namespace Task_ECommerce.API.Controllers
{
    /// <summary>
    /// Users Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        #region private fields
        private readonly IUserService _userService;
        #endregion

        #region ctor
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        /// <summary>
        /// Method to register new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        #region action methods
        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody] UserForRegistrationDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userService.RegisterAsync(request.UserName, request.Password, request.Email);

            return Ok(user);
        }

        /// <summary>
        /// Method to log in as a user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody] UserForLoginDTO request)
        {
            string token = await _userService.LoginAsync(request.UserName, request.Password);
            return Ok(token);
        }
        #endregion
    }
}
