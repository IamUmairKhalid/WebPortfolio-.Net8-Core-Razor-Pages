using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NuGet.Versioning;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class LogInModel : PageModel
    {
        private readonly AppDbContext db;
        public LogInModel(AppDbContext _db)
        {
            db = _db;
        }

        [BindProperty]
        public User user { get; set; }
        public Profile Pro { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var validuser = db.tbl_User.Where(u => u.email == user.email && u.password == user.password).FirstOrDefault();

                if (validuser != null)
                {
                    HttpContext.Session.SetString("flag", "true");
                    HttpContext.Session.SetString("username",validuser.username);
                    Pro = db.tbl_Profile.FirstOrDefault();
                    HttpContext.Session.SetString("userpic", Pro.Image);
                    return RedirectToPage("/Admin/Index");
                }
                return Page();
            }
            return Page();
        }
    }
}
