using MyOrders.Helpers;
using MyOrders.Services;
using MyOrders.Services.Abstractions;

namespace MyOrders
{
    public class App
    {
        public static void Initialize()
        {
            ServiceLocator.Instance.Register<IApiService, ApiService>();
            ServiceLocator.Instance.Register<IProductService, ProductService>();
        }
    }
}
