using AutoMapper;
using BlogWebApi.DTO;
using BlogWebApi.Helpers;
using BlogWebApi.Models;
using BlogWebApi.Repository;

namespace BlogWebApi.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }

        public async Task<ResultHelper> UploadPostAsync(PostDTO obj)
        {
            //var add = new Post()
            //{
            //    Title = obj.Title,
            //    Author = obj.Author,
            //    ContentType = obj.ContentType,
            //    Content = obj.Content,
            //    CreatedAt = DateTime.Now,
            //    CreatedBy = obj.Author
            //};

            var post = _mapper.Map<MstPost>(obj);

            post.CreatedBy = obj.Author;
            post.CreatedAt = DateTime.Now;

            var result = await _postRepository.UploadPost(post);

            if (result)
                return ResultHelper.Success("Post Uploaded Successfully..");

            return ResultHelper.Failure("Failed to upload post.. Please try again..");

        }
    }
}
