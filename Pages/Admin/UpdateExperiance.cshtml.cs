using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class UpdateExperianceModel : PageModel
    {
        AppDbContext db;

        [BindProperty]
        public Experiance exp { get; set; }
        public UpdateExperianceModel(AppDbContext _db)
        {
            db = _db;
        }
        public void OnGet(int Id)
        {
            exp = db.tbl_Experiance.Find(Id);
        }

        public IActionResult OnPost()
        {
            db.tbl_Experiance.Update(exp);
            db.SaveChanges();

            return RedirectToPage("ShowExperiance");
        }
    }
}
