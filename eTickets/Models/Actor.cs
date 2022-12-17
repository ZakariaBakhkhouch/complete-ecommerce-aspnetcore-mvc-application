
using eTickets.Data.Repository.Base;

namespace eTickets.Models
{
    public class Actor : Person, IEntityBase
    {
        [Key]
        public int Id { get; set; }

        // Relationships
        public List<Actor_Movie>? Actors_Movies { get; set; }
    }
}
