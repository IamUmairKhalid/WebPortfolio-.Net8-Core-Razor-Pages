using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class UpdateContactUsModel : PageModel
    {
        AppDbContext db;

        [BindProperty]
        public ContactUs contact { get; set; }
        public UpdateContactUsModel(AppDbContext _db)
        {
            db = _db;
        }
        public void OnGet(int Id)
        {
            contact = db.tbl_ContactUs.Find(Id);
        }

        public IActionResult OnPost()
        {
            db.tbl_ContactUs.Update(contact);
            db.SaveChanges();

            return RedirectToPage("ShowContactUs");
        }
    }
}
