using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class AddCounterModel : PageModel
    {
        AppDbContext db;

        [BindProperty]
        public Counter count { get; set; }
        public AddCounterModel(AppDbContext _db)
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
            db.tbl_Counter.Add(count);
            db.SaveChanges();

            return RedirectToPage("ShowCounter");
        }
    }
}
