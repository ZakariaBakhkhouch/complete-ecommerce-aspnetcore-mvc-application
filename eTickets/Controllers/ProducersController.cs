using eTickets.Data;
using eTickets.Repository.Base;
using eTickets.Models;
using eTickets.Repository;

namespace eTickets.Controllers
{
    public class ProducersController : Controller
    {
        private readonly IProducersRepository _producersRepository;

        public ProducersController(IProducersRepository producerRepository)
        {
            _producersRepository = producerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var producers = await _producersRepository.GetAllAsync();
            return View("Index", producers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create([Bind("ProfilePictureURL,FullName,Biography")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(producer);
            }
            await _producersRepository.AddAsync(producer);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _producersRepository.GetByIdAsync(id);
            if (result == null) return View("NotFound");
            return View("Details", result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _producersRepository.GetByIdAsync(id);
            if (result == null) return View("NotFound");
            return View("Edit", result);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Biography,ProfilePictureURL")] Producer producer)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(producer);
            }
            await _producersRepository.UpdateAsync(id, producer);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _producersRepository.GetByIdAsync(id);
            if (result == null) return View("NotFound");
            return View("Delete", result);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(Producer producer)
        {
            var actorDetails = await _producersRepository.GetByIdAsync(producer.Id);
            if (actorDetails == null) return View("NotFound");

            await _producersRepository.DeleteAsync(producer.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
