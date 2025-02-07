using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages
{
    public class IndexModel : PageModel
    {
        AppDbContext db;

        public Profile profile { get; set; }

        [BindProperty]
        public ContactUs contactUs { get; set; }
        public Social social { get; set; }
        public IEnumerable<Education> educations { get; set; }
        public IEnumerable<Experiance> experiance { get; set; }
        public IEnumerable<Counter> counter { get; set; }
        public IEnumerable<Services> services { get; set; }
        public IEnumerable<Skill> skill { get; set; }
        public IEnumerable<Testimonial> testimonials { get; set; }

        public IndexModel(AppDbContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            profile = db.tbl_Profile.FirstOrDefault();
            social = db.tbl_Social.FirstOrDefault();
            educations = db.tbl_Education;
            experiance = db.tbl_Experiance;
            counter = db.tbl_Counter;
            services = db.tbl_Services;
            skill = db.tbl_Skill;
            testimonials = db.tbl_Testimonial;

        }

        public IActionResult OnPost()
        {
            db.tbl_ContactUs.Add(contactUs);
            db.SaveChanges();

            return RedirectToAction("/");
        }
    }
}