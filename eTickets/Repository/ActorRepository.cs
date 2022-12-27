using eTickets.Repository.Base;
using eTickets.Models;
using eTickets.Data;


namespace eTickets.Repository
{
    public class ActorRepository : EntityBaseRepository<Actor>, IActorsRepository
    {
        public readonly IEntityBaseRepository<Actor> _entityBaseRepository;

        public ActorRepository(AppDbContext context) : base(context)
        {

        }
    }
}
