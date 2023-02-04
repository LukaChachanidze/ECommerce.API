using System.Security.Cryptography;
using System.Text;

namespace Task_ECommerce.Services.Encryption
{
    /// <summary>
    /// Service to verify and create password hashes
    /// </summary>
    public class EncryptionService : IEncryptionService
    {
        /// <summary>
        /// Creates password hash
        /// </summary>
        /// <param name="password"></param>
        /// <returns>hashed password</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public string CreatePasswordHash(string password)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password is null");

            try
            {
                using (var sha512 = SHA512.Create())
                {
                    byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                    byte[] passwordHash = sha512.ComputeHash(passwordBytes);

                    return Convert.ToBase64String(passwordHash);
                }
            }
            catch(Exception ex)
            {
                throw;
                //Would use _loggingService to log like this _loggingService.LogError("Error occured", ex) etc
            }
        }

        /// <summary>
        /// Verifies hashed password
        /// </summary>
        /// <param name="password"></param>
        /// <param name="hash"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public bool VerifyPasswordHash(string password, string hash)
        {
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException("password is null!");

            try
            {
                string passwordHash = CreatePasswordHash(password);
                return hash == passwordHash;
            }
            catch(Exception ex)
            {
                throw;
                //Would use _loggingService to log like this _loggingService.LogError("Error occured", ex) etc
            }
        }
    }
}
