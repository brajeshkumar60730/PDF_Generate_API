using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkSP_Demo.Entities
{
    public class ImageUpload
    {
        [ForeignKey("ImageUploads/DownloadFile")]
        public int? Id { get; set; }
        public string ImagePath { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? DownloadFile {  get; set; } 
        [NotMapped]
        public IFormFile FormFile { get; set; }
        
    }
}
