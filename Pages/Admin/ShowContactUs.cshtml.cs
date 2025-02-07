using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class ShowContactUsModel : PageModel
    {
        AppDbContext db;

        public ShowContactUsModel(AppDbContext _db)
        {
            db = _db;
        }
        public IList<ContactUs> contact { get; set; }
        public void OnGet()
        {
            contact = db.tbl_ContactUs.ToList();
        }

        public IActionResult OnGetDelete(int Id)
        {
            var ToDel = db.tbl_ContactUs.Find(Id);
            db.tbl_ContactUs.Remove(ToDel);
            db.SaveChanges();

            return RedirectToPage("ShowContactUs");
        }

    }
}
