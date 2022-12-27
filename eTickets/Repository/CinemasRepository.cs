using eTickets.Repository.Base;
using eTickets.Models;
using eTickets.Data;

namespace eTickets.Repository
{
    public class CinemasRepository : EntityBaseRepository<Cinema>, ICinemasRepository
    {
        public readonly IEntityBaseRepository<Cinema> _entityBaseRepository;

        public CinemasRepository(AppDbContext context) : base(context)
        {

        }
    }
}
