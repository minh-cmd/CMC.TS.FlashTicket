using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CMC.TS.FT.Api.HelperClass
{
    public static class JwtGenerate
    {
        public static string CreateToken(IConfiguration _configure ,string userEmail, string userName, List<string?> Permissions, string userId)
        {
            //lấy trong appsetting
            string? expireTime = _configure["JwtSetting:ExpiryMinutes"];
            string? audience = _configure["JwtSetting:Audience"];
            string? issuer = _configure["JwtSetting:Issuer"];
            string secretkey = _configure["JwtSetting:SecretKey"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretkey));
            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Email, userEmail)
            };

            foreach (var permission in Permissions)
            {
                claim.Add(new Claim(ClaimTypes.Role, permission));
            }

            ClaimsIdentity identity = new ClaimsIdentity(claim);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(expireTime)),
                Audience = audience,
                Issuer = issuer,
                SigningCredentials = creds
            };
            //
            var tokenHandler = new JwtSecurityTokenHandler();
            
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //phần này xử lý tất cả .WriteToken
            return tokenHandler.WriteToken(token);
        }
    }
}
