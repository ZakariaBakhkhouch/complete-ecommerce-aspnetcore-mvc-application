using eTickets.Data.Repository.Base;
using eTickets.Models;

namespace eTickets.Data.Repository
{
    public class ActorRepository : EntityBaseRepository<Actor>, IActorsRepository
    {
        public readonly IEntityBaseRepository<Actor> _entityBaseRepository;

        public ActorRepository(AppDbContext context) : base(context)
        {

        }
    }
}
