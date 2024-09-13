using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "New Password is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [Compare("NewPassword", ErrorMessage = "Confirm Password Does not Match password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }

		public string Email { get; set; }
	}
}
