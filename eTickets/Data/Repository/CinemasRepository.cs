using eTickets.Data.Repository.Base;
using eTickets.Models;

namespace eTickets.Data.Repository
{
    public class CinemasRepository : EntityBaseRepository<Cinema>, ICinemasRepository
    {
        public readonly IEntityBaseRepository<Cinema> _entityBaseRepository;

        public CinemasRepository(AppDbContext context) : base(context)
        {

        }
    }
}
