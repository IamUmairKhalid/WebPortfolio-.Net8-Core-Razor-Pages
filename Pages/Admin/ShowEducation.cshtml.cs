using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class ShowEducationModel : PageModel
    {
        AppDbContext db;

        public ShowEducationModel(AppDbContext _db)
        {
            db = _db;
        }
        public IList<Education> edu { get; set; }
        public void OnGet()
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            if (HttpContext.Session.GetString("flag") != "true")
            {
                HttpContext.Response.Redirect("/Admin/LogIn");
            }
            ViewData["username"] = HttpContext.Session.GetString("username");
            ViewData["pic"] = HttpContext.Session.GetString("userpic");
            edu = db.tbl_Education.ToList();
        }

        public IActionResult OnGetDelete(int Id)
        {
            var ToDel = db.tbl_Education.Find(Id);
            db.tbl_Education.Remove(ToDel);
            db.SaveChanges();

            return RedirectToPage("ShowEducation");
        }
    }
}
