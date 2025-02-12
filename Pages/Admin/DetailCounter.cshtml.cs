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
    public class DetailCounterModel : PageModel
    {
        private readonly ResumeWebApp.Data.AppDbContext _context;

        public DetailCounterModel(ResumeWebApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public Counter Counter { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Counter = await _context.tbl_Counter.FirstOrDefaultAsync(m => m.Id == id);

            if (Counter == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
