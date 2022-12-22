using eTickets.Data.Enums;
using eTickets.Data.Repository.Base;
using eTickets.Models;
using eTickets.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data.Repository
{
    public class MoviesRepository : EntityBaseRepository<Movie>, IMoviesRepository
    {
        //public readonly IEntityBaseRepository<Movie> _entityBaseRepository;

        private readonly AppDbContext _context;

        public MoviesRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movies
                .Include(c => c.Cinema)
                .Include(p => p.Producer)
                .Include(am => am.Actors_Movies).ThenInclude(a => a.Actor)
                .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task AddMovieAsync(AddMovieViewModel model)
        {
            // add the new movie
            var movie = new Movie
            {
                Name = model.Name,
                Description = model.Description,
                ImageURL = model.ImageURL,
                EndDate = model.EndDate,
                StartDate = model.StartDate,
                Price = model.Price,
                MovieCategory = model.MovieCategory,
                CinemaId = model.CinemaId,
                ProducerId = model.ProducerId,
            };

            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();

            // add the actor_movie 
            foreach(var actorId in model.ActorIds)
            {
                var actor_movie = new Actor_Movie
                {
                    MovieId = movie.Id,
                    ActorId = actorId,
                };
                await _context.Actors_Movies.AddAsync(actor_movie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMovieAsync(AddMovieViewModel model)
        {
            // get the movie
            var movie = await _context.Movies.FirstOrDefaultAsync(n => n.Id == model.Id);

            // add the new movie
            if (movie != null)
            {
                movie.Name = model.Name;
                movie.Description = model.Description;
                movie.ImageURL = model.ImageURL;
                movie.EndDate = model.EndDate;
                movie.StartDate = model.StartDate;
                movie.Price = model.Price;
                movie.MovieCategory = model.MovieCategory;
                movie.CinemaId = model.CinemaId;
                movie.ProducerId = model.ProducerId;

                await _context.SaveChangesAsync();
            }

            // remove existing actors
            var existingActorsDb = _context.Actors_Movies.Where(n => n.MovieId == model.Id).ToList();
            _context.Actors_Movies.RemoveRange(existingActorsDb);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var actorId in model.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = model.Id,
                    ActorId = actorId
                };
                await _context.Actors_Movies.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<AddMovieDropdownsViewModel> GetNewMovieDropdownsValues()
        {
            var response = new AddMovieDropdownsViewModel()
            {
                Actors = await _context.Actors.OrderBy(n => n.FullName).ToListAsync(),
                Cinemas = await _context.Cinemas.OrderBy(n => n.Name).ToListAsync(),
                Producers = await _context.Producers.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }


    }
}
