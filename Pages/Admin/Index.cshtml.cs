using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ResumeWebApp.Data;
using ResumeWebApp.Models;

namespace ResumeWebApp.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext db;

        
        public IndexModel(AppDbContext _db)
        {
            db = _db; 
        }

        public IList<ContactUs> contact { get; set; } = new List<ContactUs>();
        public IList<Skill> TopSkills { get; set; } = new List<Skill>();
        public IList<DashboardAction> QuickActions { get; set; } = new List<DashboardAction>();
        public IList<string> ContentGaps { get; set; } = new List<string>();
        public int MessageCount { get; set; }
        public int EducationCount { get; set; }
        public int ExperianceCount { get; set; }
        public int ServicesCount { get; set; }
        public int SkillCount { get; set; }
        public int CounterCount { get; set; }
        public int TestimonialCount { get; set; }
        public int SocialCount { get; set; }
        public int ProfileCount { get; set; }
        public int PortfolioHealth { get; set; }
        public int AverageSkillPower { get; set; }
        public int TotalCounterNumber { get; set; }
        public string PrimaryProfileName { get; set; } = "Portfolio owner";
        public string LeadSubject { get; set; } = "No messages yet";
        public string LeadSender { get; set; } = "Inbox is clear";
        public int[] ContentChartData { get; set; } = Array.Empty<int>();

        public async Task OnGetAsync()
        {
            if (HttpContext.Session.GetString("flag") != "true")
            {
                HttpContext.Response.Redirect("/Admin/LogIn");
                return;
            }

            ViewData["username"] = HttpContext.Session.GetString("username");
            ViewData["pic"] = HttpContext.Session.GetString("userpic");

            contact = await db.tbl_ContactUs
                .OrderByDescending(x => x.Id)
                .Take(5)
                .ToListAsync();

            MessageCount = await db.tbl_ContactUs.CountAsync();
            EducationCount = await db.tbl_Education.CountAsync();
            ExperianceCount = await db.tbl_Experiance.CountAsync();
            ServicesCount = await db.tbl_Services.CountAsync();
            SkillCount = await db.tbl_Skill.CountAsync();
            CounterCount = await db.tbl_Counter.CountAsync();
            TestimonialCount = await db.tbl_Testimonial.CountAsync();
            SocialCount = await db.tbl_Social.CountAsync();
            ProfileCount = await db.tbl_Profile.CountAsync();
            TotalCounterNumber = await db.tbl_Counter.SumAsync(x => (int?)x.Number) ?? 0;
            AverageSkillPower = (int)Math.Round(await db.tbl_Skill.AverageAsync(x => (double?)x.Number) ?? 0);

            var profile = await db.tbl_Profile.FirstOrDefaultAsync();
            if (!string.IsNullOrWhiteSpace(profile?.Name))
            {
                PrimaryProfileName = profile.Name;
            }

            var lead = contact.FirstOrDefault();
            if (lead != null)
            {
                LeadSubject = string.IsNullOrWhiteSpace(lead.Subject) ? "Message without subject" : lead.Subject;
                LeadSender = string.IsNullOrWhiteSpace(lead.Name) ? lead.Email : lead.Name;
            }

            TopSkills = await db.tbl_Skill
                .OrderByDescending(x => x.Number)
                .Take(4)
                .ToListAsync();

            ContentChartData = new[]
            {
                EducationCount,
                ExperianceCount,
                ServicesCount,
                SkillCount,
                TestimonialCount,
                SocialCount
            };

            BuildContentGaps(profile);
            BuildQuickActions();
            PortfolioHealth = CalculatePortfolioHealth();
        }

        private void BuildContentGaps(Profile? profile)
        {
            if (profile == null || string.IsNullOrWhiteSpace(profile.Name))
            {
                ContentGaps.Add("Complete the main profile so the portfolio has a clear identity.");
            }

            if (SkillCount < 5)
            {
                ContentGaps.Add("Add more skills to make the skill section feel stronger.");
            }

            if (ServicesCount < 3)
            {
                ContentGaps.Add("Add at least three services to explain what visitors can hire you for.");
            }

            if (TestimonialCount == 0)
            {
                ContentGaps.Add("Add a testimonial to build trust on the homepage.");
            }

            if (SocialCount == 0)
            {
                ContentGaps.Add("Add social links so visitors have a next step after viewing work.");
            }

            if (!ContentGaps.Any())
            {
                ContentGaps.Add("Portfolio content looks balanced. Review messages and keep details fresh.");
            }
        }

        private int CalculatePortfolioHealth()
        {
            var score = 0;
            score += ProfileCount > 0 ? 15 : 0;
            score += EducationCount > 0 ? 10 : 0;
            score += ExperianceCount > 0 ? 10 : 0;
            score += ServicesCount >= 3 ? 15 : ServicesCount * 5;
            score += SkillCount >= 5 ? 15 : SkillCount * 3;
            score += CounterCount > 0 ? 10 : 0;
            score += TestimonialCount > 0 ? 15 : 0;
            score += SocialCount > 0 ? 10 : 0;

            return Math.Min(score, 100);
        }

        private void BuildQuickActions()
        {
            QuickActions = new List<DashboardAction>
            {
                new("Profile", "Update the public intro, image, and contact details.", "Profile", "fa-user-edit", ProfileCount > 0 ? "Review" : "Add"),
                new("Skills", "Tune skill strengths and keep your strongest abilities visible.", "ShowSkill", "fa-bolt", $"{SkillCount} live"),
                new("Services", "Keep offers sharp so visitors understand your value quickly.", "ShowServices", "fa-briefcase", $"{ServicesCount} live"),
                new("Inbox", "Open visitor messages and respond while the lead is warm.", "ShowContactUs", "fa-envelope-open-text", $"{MessageCount} messages")
            };
        }

        public class DashboardAction
        {
            public DashboardAction(string title, string description, string pageName, string icon, string badge)
            {
                Title = title;
                Description = description;
                PageName = pageName;
                Icon = icon;
                Badge = badge;
            }

            public string Title { get; }
            public string Description { get; }
            public string PageName { get; }
            public string Icon { get; }
            public string Badge { get; }
        }
    }
}
