using eTickets.Repository.Base;
using eTickets.Models;
using eTickets.ViewModels;

namespace eTickets.Repository
{
    public interface IMoviesRepository : IEntityBaseRepository<Movie>
    {
        Task<Movie> GetMovieByIdAsync(int id);
        Task<AddMovieDropdownsViewModel> GetNewMovieDropdownsValues();
        Task AddMovieAsync(AddMovieViewModel model);
        Task UpdateMovieAsync(AddMovieViewModel model);
    }
}
