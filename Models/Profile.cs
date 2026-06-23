using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResumeWebApp.Models
{
    public class Profile
    {
        public int Id { get; set; }
        public string? Name{ get; set; }
        public string? Birthday{get; set;}
        public string? Email{ get; set; }
        public string? Phone {  get; set; }
        public string? Address { get; set; }
        public string? FreelanceStatus {  get; set; }
        public int Age {  get; set; }
        public string? Degree { get; set; }
        public string? Image { get; set; }
        public string? DesOne { get; set; }
        public string? DesTwo { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}
