using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeWebApp.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Review { get; set; }
        public string Designation { get; set; }
        public string Image { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
