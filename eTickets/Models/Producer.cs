using eTickets.Data.Repository.Base;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace eTickets.Models
{
    public class Producer : Person, IEntityBase
    {
        [Key]
        public int Id { get; set; }

        // Relationships
        public List<Movie>? Movies { get; set; }
    }
}
