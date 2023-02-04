using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Task_ECommerce.Domain.Entities;
using Task_ECommerce.Domain.Shared;

namespace Task_ECommerce.Services.Jwt
{
    /// <summary>
    /// Implementation of Jwt Service
    /// </summary>
    public class JwtService : IJwtService
    {
        #region private fields
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly JwtIssuerOptions _jwtIssuerOptions;
        #endregion

        #region ctor
        public JwtService(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _jwtIssuerOptions = jwtOptions.Value;
        }
        #endregion

        #region public methods
        public string GenerateToken(User user)
        {
            try
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtIssuerOptions.SecretKey));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _jwtIssuerOptions.Issuer,
                    audience: _jwtIssuerOptions.Audience,
                    claims: claims,
                    expires: DateTime.Now.AddDays(7),
                    signingCredentials: credentials
                );

                return _jwtSecurityTokenHandler.WriteToken(token);
            }
            catch(Exception ex)
            {
                throw;
                //Would use _loggingService to log like this _loggingService.LogError("Error occured", ex) etc
            }
        }
        #endregion
    }
}
