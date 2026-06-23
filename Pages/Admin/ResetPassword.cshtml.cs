using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ResumeWebApp.Data;
using System.ComponentModel.DataAnnotations;

namespace ResumeWebApp.Pages.Admin
{
    public class ResetPasswordModel : PageModel
    {
        private readonly AppDbContext db;

        public ResetPasswordModel(AppDbContext _db)
        {
            db = _db;
        }

        [BindProperty]
        [Required]
        public string Email { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "OTP is required")]
        public string Otp { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Confirm password is required")]
        [Compare("Password", ErrorMessage = "Password and confirm password do not match")]
        public string ConfirmPassword { get; set; }

        public void OnGet(string email)
        {
            Email = email;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var otp = db.tbl_EmailOtp
                .Where(o => o.Email == Email && o.Code == Otp && o.Purpose == "ResetPassword" && o.IsUsed == false && o.ExpiresAt >= DateTime.Now)
                .OrderByDescending(o => o.Id)
                .FirstOrDefault();

            if (otp == null)
            {
                ModelState.AddModelError("", "Invalid or expired OTP.");
                return Page();
            }

            var validuser = db.tbl_User.Where(u => u.email == Email).FirstOrDefault();

            if (validuser == null)
            {
                ModelState.AddModelError("", "No account found with this email.");
                return Page();
            }

            validuser.password = Password;
            otp.IsUsed = true;
            db.tbl_User.Update(validuser);
            db.tbl_EmailOtp.Update(otp);
            db.SaveChanges();

            TempData["Message"] = "Password changed successfully. Please login.";
            return RedirectToPage("LogIn");
        }
    }
}
