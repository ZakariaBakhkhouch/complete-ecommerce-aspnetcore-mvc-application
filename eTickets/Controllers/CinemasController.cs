using eTickets.Data;
using eTickets.Models;
using eTickets.Repository;

namespace eTickets.Controllers
{
    public class CinemasController : Controller
    {
        private readonly ICinemasRepository _cinemasRepository;

        public CinemasController(ICinemasRepository cinemasRepository)
        {
            _cinemasRepository = cinemasRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cinemas = await _cinemasRepository.GetAllAsync();
            return View("Index", cinemas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(cinema);
            }
            await _cinemasRepository.AddAsync(cinema);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _cinemasRepository.GetByIdAsync(id);
            if (result == null) return View("NotFound");
            return View("Details", result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _cinemasRepository.GetByIdAsync(id);
            if (result == null) return View("NotFound");
            return View("Edit", result);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Cinema cinema)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(cinema);
            }
            await _cinemasRepository.UpdateAsync(id, cinema);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _cinemasRepository.GetByIdAsync(id);
            if (result == null) return View("NotFound");
            return View("Delete", result);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(Cinema cinema)
        {
            var actorDetails = await _cinemasRepository.GetByIdAsync(cinema.Id);
            if (actorDetails == null) return View("NotFound");

            await _cinemasRepository.DeleteAsync(cinema.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
