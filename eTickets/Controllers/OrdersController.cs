using eTickets.Data;
using eTickets.Repository;
using eTickets.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eTickets.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IMoviesRepository _movieRepository;
        private readonly IOrdersRepository _ordersRepository;

        private readonly ShoppingCart _shoppingCart;

        public OrdersController(IMoviesRepository moviesRepository,ShoppingCart shoppingCart, IOrdersRepository ordersRepository)
        {
            _movieRepository= moviesRepository;
            _shoppingCart= shoppingCart;
            _ordersRepository= ordersRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var orders =  await _ordersRepository.GetOrdersByUserIdAsync("");
            return View(orders);
        }

        [HttpGet]
        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal(),
            };
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _movieRepository.GetMovieByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        [HttpPost]
        public async Task<IActionResult> RemoveItemToShoppingCart(int id)
        {
            var item = await _movieRepository.GetMovieByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        [HttpGet]
        public IActionResult OrderCompleted()
        {
            return View();
        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = "";// User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = "";// User.FindFirstValue(ClaimTypes.Email);

            await _ordersRepository.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();

            return View(nameof(OrderCompleted));
        }
    }
}
