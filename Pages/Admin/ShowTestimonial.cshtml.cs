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
    public class ShowTestimonialModel : PageModel
    {
        private readonly ResumeWebApp.Data.AppDbContext _context;

        public ShowTestimonialModel(ResumeWebApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Testimonial> Testimonial { get;set; }

        public async Task OnGetAsync()
        {
            Testimonial = await _context.tbl_Testimonial.ToListAsync();
        }
    }
}
