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
    public class DeleteExperianceModel : PageModel
    {
        private readonly ResumeWebApp.Data.AppDbContext _context;

        public DeleteExperianceModel(ResumeWebApp.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Experiance Experiance { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            ViewData["username"] = HttpContext.Session.GetString("username");
            if (HttpContext.Session.GetString("flag") != "true")
            {
                HttpContext.Response.Redirect("/Admin/LogIn");
            }
            ViewData["username"] = HttpContext.Session.GetString("username");
            ViewData["pic"] = HttpContext.Session.GetString("userpic");
            if (id == null)
            {
                return NotFound();
            }

            Experiance = await _context.tbl_Experiance.FirstOrDefaultAsync(m => m.Id == id);

            if (Experiance == null)
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

            Experiance = await _context.tbl_Experiance.FindAsync(id);

            if (Experiance != null)
            {
                _context.tbl_Experiance.Remove(Experiance);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ShowExperiance");
        }
    }
}
