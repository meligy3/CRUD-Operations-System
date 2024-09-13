using Company.DAL.Models;
using Company.PL.Helpers;
using Company.PL.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Company.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> userManager;
		private readonly SignInManager<ApplicationUser> signInManager;

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
			this.userManager = userManager;
			this.signInManager = signInManager;
		}

		#region Register

		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid) // server side validation 
			{
				var user = new ApplicationUser()
				{
					Email = model.Email,
					UserName = model.Email.Split('@')[0],
					FName = model.Email,
					LName = model.Email,
					IsAgree = model.IsAgree,
				};
				var Result = await userManager.CreateAsync(user, model.Password);
				if (Result.Succeeded)
					return RedirectToAction(nameof(Login));

				foreach (var error in Result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);

			}
			return View(model);

		}

		#endregion

		#region Login

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]

		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = await userManager.FindByEmailAsync(model.Email);
				if (user is not null)
				{
					var flag = await userManager.CheckPasswordAsync(user, model.Password);
					if (flag)
					{
						var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
						if (result.Succeeded)
							return RedirectToAction("Index", "Home");
					}
					ModelState.AddModelError(string.Empty, "Password is Invalid");
				}
				ModelState.AddModelError(string.Empty, "Email is Invalid");

			}
			return View(model);
		}

		#endregion

		#region SignOut

		public new async Task <IActionResult> SignOut()
		{
			await signInManager.SignOutAsync();	
			return RedirectToAction(nameof(Login));
		}

		#endregion


		public IActionResult ForgetPassword()
		{
			return View();
		}
		// post
		[HttpPost]
		public async Task<IActionResult> SendEmail (ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)  // server side valiation
			{
				var user = await userManager.FindByEmailAsync (model.Email);
				if (user is not null) 
				{ // Account/ResetPassword // Token
					var Token = await userManager.GeneratePasswordResetTokenAsync(user); // token vaild for one time 
					var PasswordResetLink = Url.Action("ResetPassword", "Account", new { email = user.Email,token = Token }, Request.Scheme);
                  //.https://localhost:44379/Account/ResetPassword?email=mohamed12345meligy@gmail.com&fdfddfdfdfmddmffmdmfdfd3ffcfcf

                    var email = new Email()
					{
						Subject = "Reset Password",
						To = user.Email,
						Body = "PasswordResetLink"

					}; // helper function => static function
					EmailSettings.SendEmail(email);
					return RedirectToAction(nameof(CheckYourInbox));
				
				}
				ModelState.AddModelError(string.Empty, "Email is not valid");
			}
			return View(model);
		}
		public IActionResult CheckYourInbox()
		{
			return View();
		}
		// httpget
		public IActionResult ResetPassword(string email,string token)
		{
			TempData["email"] = email;
			TempData["token"] = token;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (!ModelState.IsValid)
			{
				string email = TempData["email"]as string;
				string token = TempData["token"]as string;
				var user = await userManager.FindByEmailAsync(email);

				var result = await userManager.ResetPasswordAsync(user,token,model.NewPassword);
				
				if (result.Succeeded) 
				return RedirectToAction(nameof(Login));

				foreach(var error in result.Errors)
					ModelState.AddModelError(string.Empty , error.Description);
			}
			
			return View(model);
		}
	}
}
