using MyOrders.Helpers;
using MyOrders.Models;
using MyOrders.Services;
using MyOrders.Services.Abstractions;

namespace MyOrders
{
    public class App
    {
        public static Cart Cart { get; set; }

        public static void Initialize()
        {
            if (Cart is null)
                Cart = new Cart();
            ServiceLocator.Instance.Register<IApiService, ApiService>();
            ServiceLocator.Instance.Register<IProductService, ProductService>();
            ServiceLocator.Instance.Register<ICartService, CartService>();
        }
    }
}
