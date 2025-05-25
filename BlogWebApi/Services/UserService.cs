using BlogWebApi.DTO;
using BlogWebApi.Helpers;
using BlogWebApi.Models;
using BlogWebApi.Repository;

namespace BlogWebApi.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<ResultHelper> RegisterUserAsync(RegisterDto dto)
        {
            if (await _userRepo.UserExistsAsync(dto.Email))
            {
                return ResultHelper.Failure("User Already Exists.");
            }

            var user = new ApplicationUser()
            {
                UserName = dto.FullName.Split(' ')[0].Trim(),
                FullName = dto.FullName,
                Email = dto.Email,
                MobileNo = dto.MobileNo,

            };

            var result = await _userRepo.CreateAsync(user, dto.Password);

            if (result.Succeeded)
                return ResultHelper.Success("User Registration Successfull", new { user.Id, user.Email });


            else
                return ResultHelper.Failure(string.Join("; ", result.Errors.Select(e => e.Description)));




        }
    }
}
