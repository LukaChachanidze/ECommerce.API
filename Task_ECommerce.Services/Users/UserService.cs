using Task_ECommerce.Domain.Entities;
using Task_ECommerce.Repository.UsersRepository;
using Task_ECommerce.Services.Encryption;
using Task_ECommerce.Services.Jwt;
using Task_ECommerce.Services.Users.DTO;

namespace Task_ECommerce.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        private readonly IEncryptionService _encryptionService;

        public UserService(IUserRepository userRepository, IJwtService jwtService, IEncryptionService encryptionService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
            _encryptionService = encryptionService;
        }

        /// <summary>
        /// Login method, checks hashed password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>JWT Token</returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> LoginAsync(string userName, string password)
        {
            try
            {
                var user = await _userRepository.GetByUserNameAsync(userName);

                if (user is null)
                {
                    throw new Exception("User not found.");
                }

                if (!_encryptionService.VerifyPasswordHash(password, user.PasswordHash))
                {
                    throw new Exception("Wrong password.");
                }

                string token = _jwtService.GenerateToken(user);

                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Error occured while creating token");
                }
                return token;

            }
            catch (Exception ex)
            {
                throw;
                //Would use _loggingService to log like this _loggingService.LogError("Error occured", ex) etc
            }
        }

        /// <summary>
        /// Registers new user, creates has password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserForRegistrationDto> RegisterAsync(string userName, string password, string email)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Password is required.");
            }

            try
            {
                if (await _userRepository.GetByUserNameAsync(userName) != null)
                {
                    throw new Exception("User with that username already exists. Try other username");
                }


                string passwordHash = _encryptionService.CreatePasswordHash(password);

                var user = new User
                {
                    UserName = userName,
                    Email = email,
                    PasswordHash = passwordHash,
                };

                await _userRepository.RegisterAsync(user.UserName, user.PasswordHash, user.Email);

                var userDto = new UserForRegistrationDto()
                {
                    Email = email,
                    Password = password,
                    UserName = userName
                };

                return userDto;
            }
            catch(Exception ex)
            {
                throw;
                //Would use _loggingService to log like this _loggingService.LogError("Error occured", ex) etc
            }
        }
    }
}