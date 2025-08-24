using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class UpdateCounterModel : PageModel
    {
        AppDbContext db;

        [BindProperty]
        public Counter count { get; set; }
        public UpdateCounterModel(AppDbContext _db)
        {
            db = _db;
        }
        public void OnGet(int Id)
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            if (HttpContext.Session.GetString("flag") != "true")
            {
                HttpContext.Response.Redirect("/Admin/LogIn");
            }
            ViewData["username"] = HttpContext.Session.GetString("username");
            ViewData["pic"] = HttpContext.Session.GetString("userpic");
            count = db.tbl_Counter.Find(Id);
        }

        public IActionResult OnPost()
        {
            db.tbl_Counter.Update(count);
            db.SaveChanges();

            return RedirectToPage("ShowCounter");
        }
    }
}
