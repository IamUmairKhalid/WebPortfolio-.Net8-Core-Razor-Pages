using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class AddEducationModel : PageModel
    {
        AppDbContext db;

        [BindProperty]
        public Education edu { get; set; }
        public AddEducationModel(AppDbContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            if (HttpContext.Session.GetString("flag") != "true")
            {
                HttpContext.Response.Redirect("/Admin/LogIn");
            }
            ViewData["username"] = HttpContext.Session.GetString("username");
            ViewData["pic"] = HttpContext.Session.GetString("userpic");
        }

        public IActionResult OnPost()
        {
            db.tbl_Education.Add(edu);
            db.SaveChanges();

            return RedirectToPage("ShowEducation");
        }
    }
}
