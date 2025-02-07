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
    public class DeleteContactUsModel : PageModel
    {
        private readonly ResumeWebApp.Data.AppDbContext _context;

        public DeleteContactUsModel(ResumeWebApp.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ContactUs ContactUs { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContactUs = await _context.tbl_ContactUs.FirstOrDefaultAsync(m => m.Id == id);

            if (ContactUs == null)
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

            ContactUs = await _context.tbl_ContactUs.FindAsync(id);

            if (ContactUs != null)
            {
                _context.tbl_ContactUs.Remove(ContactUs);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./ShowContactUs");
        }
    }
}
