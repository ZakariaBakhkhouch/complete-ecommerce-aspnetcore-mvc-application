using eTickets.Data;
using eTickets.Data.Repository;
using eTickets.Models;

namespace eTickets.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsRepository _actorsRepository;

        public ActorsController(IActorsRepository actorsRepository)
        {
            _actorsRepository= actorsRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var actors = await _actorsRepository.GetAllAsync();
            return View("Index", actors);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(actor);
            }
            await _actorsRepository.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _actorsRepository.GetByIdAsync(id);
            if (result == null) return View("NotFound");
            return View("Details",result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _actorsRepository.GetByIdAsync(id);
            if (result == null) return View("NotFound");
            return View("Edit", result);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,Biography,ProfilePictureURL")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return View(actor);
            }
            await _actorsRepository.UpdateAsync(id, actor);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _actorsRepository.GetByIdAsync(id);
            if (result == null) return View("NotFound");
            return View("Delete", result);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(Actor actor)
        {
            var actorDetails = await _actorsRepository.GetByIdAsync(actor.Id);
            if (actorDetails == null) return View("NotFound");

            await _actorsRepository.DeleteAsync(actor.Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
