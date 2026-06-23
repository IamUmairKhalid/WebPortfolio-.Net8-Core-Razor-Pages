using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using ResumeWebApp.Models;
using ResumeWebApp.Mail;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace ResumeWebApp.Pages.Admin
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly AppDbContext db;
        private readonly IEmailSender emailSender;

        public ForgotPasswordModel(AppDbContext _db, IEmailSender _emailSender)
        {
            db = _db;
            emailSender = _emailSender;
        }

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var validuser = db.tbl_User.Where(u => u.email == Email).FirstOrDefault();

            if (validuser == null)
            {
                ModelState.AddModelError("", "No account found with this email.");
                return Page();
            }

            string code = GenerateOtp();

            db.tbl_EmailOtp.Add(new EmailOtp
            {
                Email = Email,
                Code = code,
                Purpose = "ResetPassword",
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(10),
                IsUsed = false
            });
            await db.SaveChangesAsync();

            await emailSender.SendEmailAsync(Email, "Password Reset OTP", $"Your password reset OTP is {code}. This code will expire in 10 minutes.");

            TempData["Message"] = "OTP sent to your email.";
            return RedirectToPage("ResetPassword", new { email = Email });
        }

        private string GenerateOtp()
        {
            return RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        }
    }
}
