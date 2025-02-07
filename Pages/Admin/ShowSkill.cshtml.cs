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
    public class ShowSkillModel : PageModel
    {
        private readonly ResumeWebApp.Data.AppDbContext _context;

        public ShowSkillModel(ResumeWebApp.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<Skill> Skill { get;set; }

        public async Task OnGetAsync()
        {
            Skill = await _context.tbl_Skill.ToListAsync();
        }
    }
}
