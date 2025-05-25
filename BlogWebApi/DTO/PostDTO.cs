namespace BlogWebApi.DTO
{
    public class PostDTO
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string ContentType { get; set; }

        public string filePath { get; set; }

        public IFormFile File { get; set; }
    }
}
