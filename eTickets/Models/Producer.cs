namespace eTickets.Models
{
    public class Producer : Person
    {
        [Key]
        public int Id { get; set; }

        // Relationships
        public List<Movie> Movies { get; set; }
    }
}
