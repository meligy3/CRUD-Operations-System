using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
	public class RegisterViewModel
	{
		[Required(ErrorMessage ="First Name is Required")]
		public string FName { get; set; }

		[Required(ErrorMessage = "Last Name is Required")]
		public string LName { get; set; }

		[Required(ErrorMessage = "Email is Required")]
		[EmailAddress(ErrorMessage ="Invalid Email")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Password is Required")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		[Required(ErrorMessage = "Password is Required")]
		[Compare("Password",ErrorMessage ="Confirm Password Does not Match password")]
		[DataType(DataType.Password)]
		public string ConfirmPassword { get; set; }

		public bool IsAgree { get; set; }
	}
}
