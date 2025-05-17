using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class UpdateSkillModel : PageModel
    {
        private readonly ResumeWebApp.Data.AppDbContext _context;

        public UpdateSkillModel(ResumeWebApp.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Skill Skill { get; set; }

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

            Skill = await _context.tbl_Skill.FirstOrDefaultAsync(m => m.Id == id);

            if (Skill == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Skill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(Skill.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./ShowSkill");
        }

        private bool SkillExists(int id)
        {
            return _context.tbl_Skill.Any(e => e.Id == id);
        }
    }
}
