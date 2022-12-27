namespace eTickets.Models
{
    public class User
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress] public string? Email { get; set; }
        

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }


        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }


        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number is required")]
        [Phone] public string? PhoneNumber { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)] public string? Password { get; set; }


        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required")]
        [Compare("Password",ErrorMessage = "Password do not match")]
        [DataType(DataType.Password)] public string? ConfirmPassword { get; set; }


    }

    public class UserLogin
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress] public string? Email { get; set; }


        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)] public string? Password { get; set; }
    }
}
