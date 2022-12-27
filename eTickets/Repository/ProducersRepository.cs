using eTickets.Repository.Base;
using eTickets.Models;
using eTickets.Data;


namespace eTickets.Repository
{
    public class ProducersRepository : EntityBaseRepository<Producer>, IProducersRepository
    {
        public readonly IEntityBaseRepository<Producer> _entityBaseRepository;

        public ProducersRepository(AppDbContext context) : base(context)
        {

        }
    }
}
