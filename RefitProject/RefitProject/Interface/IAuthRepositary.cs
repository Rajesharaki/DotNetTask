using DapperProhect.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefitProject.Interface
{
    public interface IAuthRepositary
    {
        [Post("/api/User/authenticate")]
        Task<User> GetToken(AuthenticateModel model);
        [Get("/api/User/GetAllAdmins")]
        Task<IEnumerable<User>> GetAdmins([Authorize("Bearer")]string token);
        [Get("/api/User/GetAllUsers")]
        Task<IEnumerable<User>> GetUsers([Authorize("Bearer")] string token);

    }
}
