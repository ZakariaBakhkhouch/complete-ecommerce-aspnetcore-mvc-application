namespace eTickets.Models
{
    public class Person
    {
        [Required(ErrorMessage = "Full Name is required")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Biography is required")]
        [StringLength(maximumLength: 50,MinimumLength = 5,ErrorMessage = "Full name must be between 5 and 50 chars")]
        [Display(Name = "Biography")]
        public string Biography { get; set; }

        [Required(ErrorMessage = "Picture is required")]
        [Display(Name = "Picture")]
        public string ProfilePictureURL { get; set; }
    }
}
