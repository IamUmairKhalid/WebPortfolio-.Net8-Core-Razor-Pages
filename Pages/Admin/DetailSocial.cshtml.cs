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
    public class DetailSocialModel : PageModel
    {
        private readonly ResumeWebApp.Data.AppDbContext _context;

        public DetailSocialModel(ResumeWebApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public Social Social { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            if (HttpContext.Session.GetString("flag") != "true")
            {
                HttpContext.Response.Redirect("/Admin/LogIn");
            }
            if (id == null)
            {
                return NotFound();
            }

            Social = await _context.tbl_Social.FirstOrDefaultAsync(m => m.Id == id);

            if (Social == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
