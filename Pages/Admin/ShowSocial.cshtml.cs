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
    public class ShowSocialModel : PageModel
    {
        private readonly ResumeWebApp.Data.AppDbContext _context;

        public ShowSocialModel(ResumeWebApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Social> Social { get;set; }

        public async Task OnGetAsync()
        {
            Social = await _context.tbl_Social.ToListAsync();
        }
    }
}
