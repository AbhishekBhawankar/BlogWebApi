using BlogWebApi.Models;
using Microsoft.AspNetCore.Identity;

namespace BlogWebApi.Repository
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string email);

        Task<IdentityResult> CreateAsync(ApplicationUser user, string password);


    }
}
