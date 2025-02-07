using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class AddTestimonialModel : PageModel
    {
        AppDbContext db;
        IWebHostEnvironment env;

        [BindProperty]
        public Testimonial Testimonial { get; set; }

        public AddTestimonialModel(AppDbContext _db, IWebHostEnvironment _env)
        {
            this.db = _db;
            this.env = _env;
        }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {

            string ImageName = Testimonial.Photo.FileName;

            var FolderPath = Path.Combine(env.WebRootPath, "images");
            var ImagePath = Path.Combine(FolderPath, ImageName);


            var myFileStream = new FileStream(ImagePath, FileMode.Create);

            Testimonial.Photo.CopyTo(myFileStream);
            myFileStream.Close();

            Testimonial.Image = ImageName;

            db.tbl_Testimonial.Add(Testimonial);
            db.SaveChanges();
            
            return RedirectToPage("./ShowTestimonial");

        }

    }
}
