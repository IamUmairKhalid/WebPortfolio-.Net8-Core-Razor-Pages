using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using ResumeWebApp.Models;
using ResumeWebApp.Mail;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace ResumeWebApp.Pages.Admin
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext db;
        private readonly IEmailSender emailSender;

        public RegisterModel(AppDbContext _db, IEmailSender _emailSender)
        {
            db = _db;
            emailSender = _emailSender;
        }

        [BindProperty]
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string ConfirmPassword { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (!Email.EndsWith("@gmail.com", StringComparison.OrdinalIgnoreCase))
            {
                ModelState.AddModelError("", "Please enter a valid Gmail address.");
                return Page();
            }

            var validuser = db.tbl_User.Where(u => u.email == Email).FirstOrDefault();

            if (validuser != null)
            {
                ModelState.AddModelError("", "Account already exists with this email.");
                return Page();
            }

            string code = GenerateOtp();

            db.tbl_EmailOtp.Add(new EmailOtp
            {
                Email = Email,
                Code = code,
                Purpose = "Register",
                CreatedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(10),
                IsUsed = false
            });
            await db.SaveChangesAsync();

            HttpContext.Session.SetString("RegisterUsername", Username);
            HttpContext.Session.SetString("RegisterEmail", Email);
            HttpContext.Session.SetString("RegisterPassword", Password);

            await emailSender.SendEmailAsync(Email, "Account Verification OTP", $"Your account verification OTP is {code}. This code will expire in 10 minutes.");

            TempData["Message"] = "OTP sent to your Gmail.";
            return RedirectToPage("VerifyRegisterOtp");
        }

        private string GenerateOtp()
        {
            return RandomNumberGenerator.GetInt32(100000, 999999).ToString();
        }
    }
}
