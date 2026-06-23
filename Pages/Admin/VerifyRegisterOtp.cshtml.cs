using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using ResumeWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace ResumeWebApp.Pages.Admin
{
    public class VerifyRegisterOtpModel : PageModel
    {
        private readonly AppDbContext db;

        public VerifyRegisterOtpModel(AppDbContext _db)
        {
            db = _db;
        }

        [BindProperty]
        [Required(ErrorMessage = "OTP is required")]
        public string Otp { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("RegisterEmail") == null)
            {
                return RedirectToPage("Register");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string email = HttpContext.Session.GetString("RegisterEmail");
            string username = HttpContext.Session.GetString("RegisterUsername");
            string password = HttpContext.Session.GetString("RegisterPassword");

            if (email == null || username == null || password == null)
            {
                return RedirectToPage("Register");
            }

            var otp = db.tbl_EmailOtp
                .Where(o => o.Email == email && o.Code == Otp && o.Purpose == "Register" && o.IsUsed == false && o.ExpiresAt >= DateTime.Now)
                .OrderByDescending(o => o.Id)
                .FirstOrDefault();

            if (otp == null)
            {
                ModelState.AddModelError("", "Invalid or expired OTP.");
                return Page();
            }

            var validuser = db.tbl_User.Where(u => u.email == email).FirstOrDefault();

            if (validuser != null)
            {
                ModelState.AddModelError("", "Account already exists with this email.");
                return Page();
            }

            db.tbl_User.Add(new User
            {
                username = username,
                email = email,
                password = password
            });

            otp.IsUsed = true;
            db.tbl_EmailOtp.Update(otp);
            db.SaveChanges();

            HttpContext.Session.Remove("RegisterUsername");
            HttpContext.Session.Remove("RegisterEmail");
            HttpContext.Session.Remove("RegisterPassword");

            TempData["Message"] = "Account created successfully. Please login.";
            return RedirectToPage("LogIn");
        }
    }
}
