using MyOrders.Services;
using MyOrders.Services.Abstractions;
using Xamarin.Forms;

namespace MyOrders
{
    public static class App
    {
        public static void StartUp()
        {
            DependencyService.RegisterSingleton<IApiService>(new ApiService());
        }
    }
}
