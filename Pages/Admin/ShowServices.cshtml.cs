using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class ShowServicesModel : PageModel
    {
        private readonly ResumeWebApp.Data.AppDbContext _context;

        public ShowServicesModel(ResumeWebApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Services> Services { get;set; }

        public async Task OnGetAsync()
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            if (HttpContext.Session.GetString("flag") != "true")
            {
                HttpContext.Response.Redirect("/Admin/LogIn");
            }
            Services = await _context.tbl_Services.ToListAsync();
        }
    }
}
