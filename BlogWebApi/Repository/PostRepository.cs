using BlogWebApi.Data;
using BlogWebApi.Models;

namespace BlogWebApi.Repository
{
    public class PostRepository : IPostRepository
    {

        private readonly ApplicationDbContext _coreDbContext;

        public PostRepository(ApplicationDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        public async Task<bool> UploadPost(MstPost post)
        {
            await _coreDbContext.MstPost.AddAsync(post);
            var save = await _coreDbContext.SaveChangesAsync();
            return save == 1 ? true : false;
        }
    }
}
