using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ResumeWebApp.Pages.Admin
{
    public class LogOutModel : PageModel
    {
        public RedirectToPageResult OnGet()
        {

            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}
