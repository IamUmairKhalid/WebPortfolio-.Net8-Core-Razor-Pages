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
    public class DeleteServicesModel : PageModel
    {
        private readonly ResumeWebApp.Data.AppDbContext _context;

        public DeleteServicesModel(ResumeWebApp.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Services Services { get; set; }

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

            Services = await _context.tbl_Services.FirstOrDefaultAsync(m => m.Id == id);

            if (Services == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Services = await _context.tbl_Services.FindAsync(id);

            if (Services != null)
            {
                _context.tbl_Services.Remove(Services);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ShowServices");
        }
    }
}
