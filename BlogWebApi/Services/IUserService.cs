using BlogWebApi.DTO;
using BlogWebApi.Helpers;

namespace BlogWebApi.Services
{
    public interface IUserService
    {
        Task<ResultHelper> RegisterUserAsync(RegisterDto dto);
    }
}
