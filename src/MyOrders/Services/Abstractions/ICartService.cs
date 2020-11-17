using MyOrders.Models;

namespace MyOrders.Services.Abstractions
{
    public interface ICartService
    {
        void AddProduct(Cart cart, Product product);
        void RemoveProduct(Cart cart, Product product);
        decimal ApplyDiscount(Cart cart, Product product, Sale sale);
    }
}
