using eTickets.Data.Repository.Base;
using eTickets.Models;

namespace eTickets.Data.Repository
{
    public class ProducersRepository : EntityBaseRepository<Producer>, IProducersRepository
    {
        public readonly IEntityBaseRepository<Producer> _entityBaseRepository;

        public ProducersRepository(AppDbContext context) : base(context)
        {

        }
    }
}
