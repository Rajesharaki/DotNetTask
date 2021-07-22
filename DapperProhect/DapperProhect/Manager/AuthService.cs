using DapperProhect.Interface;
using DapperProhect.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using Microsoft.Extensions.Caching.Memory;

namespace DapperProhect.Manager
{
    public class AuthService : IAuthService
    {
        private List<User> _users = new List<User>
        {
            new User { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = Role.Admin },
            new User { Id = 1, FirstName = "Admin1", LastName = "User1", Username = "admin1", Password = "admin1", Role = Role.Admin },
            new User { Id = 1, FirstName = "Admin2", LastName = "User2", Username = "admin2", Password = "admin2", Role = Role.Admin },

            new User { Id = 2, FirstName = "Normal", LastName = "User", Username = "user", Password = "user", Role = Role.User },
            new User { Id = 2, FirstName = "Normal1", LastName = "User1", Username = "user1", Password = "user1", Role = Role.User },
            new User { Id = 2, FirstName = "Normal2", LastName = "User2", Username = "user2", Password = "user2", Role = Role.User }
        };

        private readonly AppSettings _appSettings;

        public AuthService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task<User> GenerateToken(AuthenticateModel model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            // return null if user not found
            if (user == null)
                return null;

            return await GetToken(user);
        }
        public async Task<User> GetToken(User user)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            user.RefreshToken = GenerateRefreshToken();

            return await Task.FromResult(user);
        }

        //Genrating refresh token
        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
        public async Task<IEnumerable<User>> GetAll()
        {
            return await Task.FromResult(_users.FindAll(x => x.Role == "Admin"));
        }

        public IEnumerable<User> GetAlluser()
        {
            return _users.FindAll(x => x.Role == "User");
        }

        public TokenValidationParameters GetTokenValidationParameter()
        {
            return new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        }

        public async Task<User> GenaratedNewToken(User model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);
            if (user == null)
                return null;
            return await GetToken(model);
        }
    }
}
