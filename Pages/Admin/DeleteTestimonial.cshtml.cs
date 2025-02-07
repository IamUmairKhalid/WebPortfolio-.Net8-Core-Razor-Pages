using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class DeleteTestimonialModel : PageModel
    {
        AppDbContext db;
        IWebHostEnvironment env;

        [BindProperty]
        public Testimonial Testimonial { get; set; }

        public DeleteTestimonialModel(AppDbContext _db, IWebHostEnvironment _env)
        {
            this.db = _db;
            this.env = _env;
        }

        public void OnGet(int id)
        {
            Testimonial = db.tbl_Testimonial.Find(id);
        }

        public IActionResult OnPost(int id)
        {

                Testimonial = db.tbl_Testimonial.Find(id);

                string old_image = Testimonial.Image;
                DeletePicture(old_image);

                db.tbl_Testimonial.Remove(Testimonial);
                db.SaveChanges();

                return RedirectToPage("ShowTestimonial");
            
        }

        public void DeletePicture(string old_pic)
        {
            var FolderPath = Path.Combine(env.WebRootPath, "images");
            var ImagePath = Path.Combine(FolderPath, old_pic);

            var flag = System.IO.File.Exists(ImagePath);

            if (flag)
            {
                System.IO.File.Delete(ImagePath);
            }
        }
    }
}
