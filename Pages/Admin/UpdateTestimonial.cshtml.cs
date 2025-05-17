using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class UpdateTestimonialModel : PageModel
    {
        AppDbContext db;
        IWebHostEnvironment env;

        [BindProperty]
        public Testimonial Testimonial { get; set; }

        public UpdateTestimonialModel(AppDbContext _db, IWebHostEnvironment _env)
        {
            this.db = _db;
            this.env = _env;
        }

        public void OnGet(int id)
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            if (HttpContext.Session.GetString("flag") != "true")
            {
                HttpContext.Response.Redirect("/Admin/LogIn");
            }
            Testimonial = db.tbl_Testimonial.Find(id);
        }

        public IActionResult OnPost()
        {
            if (Testimonial.Photo is null)
            {
                db.tbl_Testimonial.Update(Testimonial);
                db.SaveChanges();
                return RedirectToPage("./ShowTestimonial");
            }
            else
            {
                string ImageName = Testimonial.Photo.FileName;

                string old_image = Testimonial.Image;
                DeletePicture(old_image);

                var FolderPath = Path.Combine(env.WebRootPath, "images");
                var ImagePath = Path.Combine(FolderPath, ImageName);

                var myFileStream = new FileStream(ImagePath, FileMode.Create);

                Testimonial.Photo.CopyTo(myFileStream);
                myFileStream.Close();

                Testimonial.Image = ImageName;

                db.tbl_Testimonial.Update(Testimonial);
                db.SaveChanges();

                return RedirectToPage("./ShowTestimonial");
            }
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
