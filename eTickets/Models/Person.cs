namespace eTickets.Models
{
    public class Person
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Biography")]
        public string Biography { get; set; }

        [Display(Name = "Picture")]
        public string ProfilePictureURL { get; set; }
    }
}
