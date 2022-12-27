using eTickets.Models;

namespace eTickets.Repository
{
    public interface IOrdersRepository
    {
        Task StoreOrderAsync(List<ShoppingCartItem> shoppingCartItems, string userId, string userEmail);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);
    }
}
