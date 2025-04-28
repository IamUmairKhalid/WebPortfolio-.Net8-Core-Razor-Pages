using Microsoft.EntityFrameworkCore;
using ResumeWebApp.Models;

namespace ResumeWebApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<ContactUs> tbl_ContactUs {  get; set; }
        public DbSet<Counter> tbl_Counter { get; set; }
        public DbSet<Education> tbl_Education { get; set; }
        public DbSet<Experiance> tbl_Experiance { get; set; }
        public DbSet<Profile> tbl_Profile { get; set; }
        public DbSet<Services> tbl_Services { get; set; }
        public DbSet<Skill> tbl_Skill { get; set; }
        public DbSet<Social> tbl_Social { get; set; }
        public DbSet<Testimonial> tbl_Testimonial { get; set; }
        public DbSet<User> tbl_User { get; set; }
    }
}
