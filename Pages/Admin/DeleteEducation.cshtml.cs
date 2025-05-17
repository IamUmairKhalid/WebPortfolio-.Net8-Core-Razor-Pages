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
    public class DeleteEducationModel : PageModel
    {
        private readonly ResumeWebApp.Data.AppDbContext _context;

        public DeleteEducationModel(ResumeWebApp.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Education Education { get; set; }

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

            Education = await _context.tbl_Education.FirstOrDefaultAsync(m => m.Id == id);

            if (Education == null)
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

            Education = await _context.tbl_Education.FindAsync(id);

            if (Education != null)
            {
                _context.tbl_Education.Remove(Education);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ShowEducation");
        }
    }
}
