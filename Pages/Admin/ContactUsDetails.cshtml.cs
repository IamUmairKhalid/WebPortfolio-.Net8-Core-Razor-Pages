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
    public class ContactUsDetailsModel : PageModel
    {
        private readonly ResumeWebApp.Data.AppDbContext _context;

        public ContactUsDetailsModel(ResumeWebApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public ContactUs ContactUs { get; set; }

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

            ContactUs = await _context.tbl_ContactUs.FirstOrDefaultAsync(m => m.Id == id);

            if (ContactUs == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
