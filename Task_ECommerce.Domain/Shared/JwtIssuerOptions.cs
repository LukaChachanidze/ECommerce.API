using Microsoft.IdentityModel.Tokens;

namespace Task_ECommerce.Domain.Shared
{
    /// <summary>
    /// Jwt Issuer Options to read from AppSettings
    /// </summary>
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecretKey { get; set; }
        public SigningCredentials SigningCredentials { get; set; }
    }
}
