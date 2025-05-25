using BlogWebApi.DTO;
using BlogWebApi.Helpers;

namespace BlogWebApi.Services
{
    public interface IPostService
    {
        Task<ResultHelper> UploadPostAsync(PostDTO obj);
    }
}
