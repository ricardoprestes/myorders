using Android.App;
using Android.Widget;
using Android.OS;
using MyOrders.ViewModels;
using MyOrders.Helpers;
using MyOrders.Services.Abstractions;
using System.Linq;
using System.Threading.Tasks;

namespace MyOrders.Droid.Activities
{
    [Activity(Label = "MyOrders",
              MainLauncher = true,
              Icon = "@mipmap/icon",
              Theme = "@style/AppTheme")]
    public class MainActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.activity_main;

        public MainViewModel ViewModel { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var apiService = ServiceLocator.Instance.Get<IApiService>();
            ViewModel = new MainViewModel(apiService);
            Toolbar.Title = ViewModel.Title;
        }

        protected override async void OnStart()
        {
            base.OnStart();
            await OnStartAsync();
        }

        async Task OnStartAsync()
        {
            if (!ViewModel.Items.Any())
                await ViewModel.LoadItemsAsync();
        }
    }
}

