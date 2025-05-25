using System.ComponentModel.DataAnnotations;

namespace BlogWebApi.Models
{
    public class MstPost
    {
        [Key]
        public int id { get; set; }

        public string Author { get; set; }

        public string ContentType { get; set; }

        public string filePath { get; set; }

        public string Title { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
