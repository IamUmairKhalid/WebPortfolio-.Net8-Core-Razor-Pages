using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class IndexModel : PageModel
    {
        AppDbContext db;

        
        public IndexModel(AppDbContext _db)
        {
            db = _db; 
        }
        public IList<ContactUs> contact { get; set; }
        public void OnGet()
        {
            if (HttpContext.Session.GetString("flag") != "true")
            {
                HttpContext.Response.Redirect("/Admin/LogIn");
            }
            ViewData["username"] = HttpContext.Session.GetString("username");
            ViewData["pic"] = HttpContext.Session.GetString("userpic");

            contact = db.tbl_ContactUs.ToList();
        }
    }
}
