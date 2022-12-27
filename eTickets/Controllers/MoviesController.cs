using eTickets.Data;
using eTickets.Models;
using eTickets.Repository;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eTickets.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesRepository _moviesRepository;

        public MoviesController(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movies = await _moviesRepository.GetAllAsync(n => n.Cinema);
            return View("Index",movies);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _moviesRepository.GetMovieByIdAsync(id);
            return View("Details", movie);
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _moviesRepository.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            return View("Create");
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddMovieViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _moviesRepository.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(model);
            }

            await _moviesRepository.AddMovieAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _moviesRepository.GetMovieByIdAsync(id);

            if(movie == null) return View("Error");

            var movieDropdownsData = await _moviesRepository.GetNewMovieDropdownsValues();

            ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
            ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
            ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

            var response = new AddMovieViewModel
            {
                Name = movie.Name,
                ImageURL = movie.ImageURL,
                Description = movie.Description,
                Price = movie.Price,
                StartDate = movie.StartDate,
                EndDate = movie.EndDate,
                CinemaId = movie.CinemaId,
                MovieCategory = movie.MovieCategory,
                ProducerId = movie.ProducerId,
                ActorIds = movie.Actors_Movies.Select(n => n.ActorId).ToList()
            };

            return View("Edit", response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AddMovieViewModel model)
        {
            if (id != model.Id) return View("Error");

            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _moviesRepository.GetNewMovieDropdownsValues();

                ViewBag.Cinemas = new SelectList(movieDropdownsData.Cinemas, "Id", "Name");
                ViewBag.Producers = new SelectList(movieDropdownsData.Producers, "Id", "FullName");
                ViewBag.Actors = new SelectList(movieDropdownsData.Actors, "Id", "FullName");

                return View(model);
            }

            await _moviesRepository.UpdateMovieAsync(model);
            return RedirectToAction(nameof(Index));
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _moviesRepository.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
                //var filteredResult = allMovies.Where(n => n.Name.ToLower().Contains(searchString.ToLower()) || n.Description.ToLower().Contains(searchString.ToLower())).ToList();

                var filteredResultNew = allMovies.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allMovies);
        }

    }
}
