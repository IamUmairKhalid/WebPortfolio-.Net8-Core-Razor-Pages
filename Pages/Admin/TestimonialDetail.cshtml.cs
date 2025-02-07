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
    public class TestimonialDetailModel : PageModel
    {
        private readonly ResumeWebApp.Data.AppDbContext _context;

        public TestimonialDetailModel(ResumeWebApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public Testimonial Testimonial { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Testimonial = await _context.tbl_Testimonial.FirstOrDefaultAsync(m => m.Id == id);

            if (Testimonial == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
