using MyOrders.Helpers;
using MyOrders.Services;
using MyOrders.Services.Abstractions;
using Xamarin.Forms;

namespace MyOrders
{
    public class App
    {
        public static void Initialize()
        {
            //DependencyService.RegisterSingleton<IApiService>(new ApiService());
            ServiceLocator.Instance.Register<IApiService, ApiService>();
        }
    }
}
