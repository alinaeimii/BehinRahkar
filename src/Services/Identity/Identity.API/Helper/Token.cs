using Identity.API.Models;
using Microsoft.IdentityModel.Tokens;
using Shared.Helper;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Identity.API.Helper
{
    public class Token
    {
        public static string GenerateToken(User user)
        {
            var rsa = RSA.Create();
            string key = RsaKey.GetPrivateKey();
            rsa.FromXmlString(key);

            var credentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

            var jwt = new JwtSecurityToken(
                new JwtHeader(credentials),
                new JwtPayload(
                    "BehinRahkar",
                    "BehinRahkar",
                    new List<Claim>() {
                       new Claim(JwtRegisteredClaimNames.Name,user.username),
                       new Claim("Role",user.role),
                       new Claim("Service",string.Join( ",", user.services)),
                    },
                    DateTime.UtcNow,
                    DateTime.UtcNow.AddHours(3)
                )
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);
            return token;
        }
    }
}
