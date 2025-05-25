using BlogWebApi.Models;

namespace BlogWebApi.Repository
{
    public interface IPostRepository
    {
        Task<bool> UploadPost(MstPost post);


    }
}
