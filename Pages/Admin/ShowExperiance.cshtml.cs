using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class ShowExperianceModel : PageModel
    {
        AppDbContext db;

        public ShowExperianceModel(AppDbContext _db)
        {
            db = _db;
        }
        public IList<Experiance> exp { get; set; }
        public void OnGet()
        {
            exp = db.tbl_Experiance.ToList();
        }

        public IActionResult OnGetDelete(int Id)
        {
            var ToDel = db.tbl_Experiance.Find(Id);
            db.tbl_Experiance.Remove(ToDel);
            db.SaveChanges();

            return RedirectToPage("ShowExperiance");
        }
    }
}
