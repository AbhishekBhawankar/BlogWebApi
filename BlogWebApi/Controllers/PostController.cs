using BlogWebApi.DTO;
using BlogWebApi.Helpers;
using BlogWebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }


        [HttpPost("UploadPost")]
        public async Task<IActionResult> UploadPost([FromForm] PostDTO obj)
        {
            try
            {
                string filepath = null;


                if (obj.File != null && obj.File.Length > 0)
                {

                    var fileExt = Path.GetExtension(obj.File.FileName);

                    var allowedExt = new[] { ".jpg", ".jpeg", ".mp4", ".txt" , ".pdf" };

                    if (!allowedExt.Contains(fileExt.ToLower()))
                        return BadRequest(new { Message = "Please choose correct file format", allowedExt });


                    var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "BlogUploadedFiles");
                    if (!Directory.Exists(folderPath))
                        Directory.CreateDirectory(folderPath);


                    var fileName = obj.Author +" "+ Guid.NewGuid().ToString() + fileExt;
                    var fullPath = Path.Combine(folderPath, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        await obj.File.CopyToAsync(stream);
                    }

                    //get relative path for db
                    filepath = Path.Combine("BlogUploadedFiles", fileName);
                    obj.filePath = filepath;

                }

                var result = await _postService.UploadPostAsync(obj);

                if (result.Status)
                    return Ok(new { result });

                return BadRequest(new { result });

            }
            catch (Exception ex)
            {
                var error = ResultHelper.Failure("An unexpected error occurred.", ex.Message);
                return StatusCode(500, error);
            }
        }
    }
}
