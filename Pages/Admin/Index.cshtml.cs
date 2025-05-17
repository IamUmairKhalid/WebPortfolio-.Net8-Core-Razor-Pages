using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ResumeWebApp.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
        }
        public void OnGet()
        {
            if (HttpContext.Session.GetString("flag") != "true")
            {
                HttpContext.Response.Redirect("/Admin/LogIn");
            }
            ViewData["username"] = HttpContext.Session.GetString("username");
            ViewData["pic"] = HttpContext.Session.GetString("userpic");
        }
    }
}
