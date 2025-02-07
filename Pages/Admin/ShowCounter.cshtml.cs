using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class ShowCounterModel : PageModel
    {
        AppDbContext db;

        public ShowCounterModel(AppDbContext _db)
        {
            db = _db;
        }
        public IList<Counter> count { get; set; }
        public void OnGet()
        {
            count = db.tbl_Counter.ToList();
        }

        public IActionResult OnGetDelete(int Id)
        {
            var ToDel = db.tbl_Counter.Find(Id);
            db.tbl_Counter.Remove(ToDel);
            db.SaveChanges();

            return RedirectToPage("ShowCounter");
        }
    }
}
