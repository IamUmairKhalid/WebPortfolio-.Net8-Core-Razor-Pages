using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.ObjectModelRemoting;
using ResumeWebApp.Data;
using ResumeWebApp.Models;
using System.Data.OleDb;

namespace ResumeWebApp.Pages.Admin
{
    public class ProfileModel : PageModel
    {
        AppDbContext db;
        IWebHostEnvironment env;

        [BindProperty]
        public Profile Pro { get; set; }

        public ProfileModel(AppDbContext _db, IWebHostEnvironment _env)
        {
            this.db = _db;
            this.env = _env;
        }

        public void OnGet()
        {
            Pro = db.tbl_Profile.FirstOrDefault();
        }

        public void OnPost()
        {
            if(Pro.Photo is null)
            {
                db.tbl_Profile.Update(Pro);
                db.SaveChanges();
            }
            else
            {
                string ImageName = Pro.Photo.FileName;

                string old_image = Pro.Image;
                DeletePicture(old_image);

                var FolderPath = Path.Combine(env.WebRootPath, "images");
                var ImagePath = Path.Combine(FolderPath, ImageName);


                var myFileStream = new FileStream(ImagePath, FileMode.Create);

                Pro.Photo.CopyTo(myFileStream);
                myFileStream.Close();

                Pro.Image = ImageName;

                db.tbl_Profile.Update(Pro);
                db.SaveChanges();

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
