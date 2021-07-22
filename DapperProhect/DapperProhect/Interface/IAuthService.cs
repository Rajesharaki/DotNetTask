using DapperProhect.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperProhect.Interface
{
    public interface IAuthService
    {
        Task<User> GenerateToken(AuthenticateModel model);
        Task<IEnumerable<User>> GetAll();
        IEnumerable<User> GetAlluser();
        TokenValidationParameters GetTokenValidationParameter();
        Task<User> GenaratedNewToken(User user);
    }
}
