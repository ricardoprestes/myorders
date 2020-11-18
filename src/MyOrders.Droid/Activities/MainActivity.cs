using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MyOrders.Droid.Adapters;
using MyOrders.Helpers;
using MyOrders.Models;
using MyOrders.Services.Abstractions;
using MyOrders.ViewModels;

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

        SwipeRefreshLayout _refresh;
        SaleProductAdapter _adapter;
        LinearLayout _llCartValue;
        Button _btnBuy;
        IMenu _menu;
        bool _subscribeEvents = false;
        bool _apiRequest;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            var apiService = ServiceLocator.Instance.Get<IApiService>();
            var productService = ServiceLocator.Instance.Get<IProductService>();
            var cartService = ServiceLocator.Instance.Get<ICartService>();
            ViewModel = new MainViewModel(apiService, productService, cartService);
            Toolbar.Title = ViewModel.Title;

            _llCartValue = FindViewById<LinearLayout>(Resource.Id.ll_cart_value);
            _btnBuy = FindViewById<Button>(Resource.Id.btn_buy);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.rv_items);
            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(_adapter = new SaleProductAdapter(this, ViewModel));

            _refresh = FindViewById<SwipeRefreshLayout>(Resource.Id.srl_items);
            _refresh.SetColorSchemeColors(Resource.Color.accent);
            _apiRequest = true;
        }

        protected override async void OnStart()
        {
            base.OnStart();
            ShowCartValue();
            if (_apiRequest)
                await LoadDataAsync().ConfigureAwait(false);
            await LoadItemsAsync().ConfigureAwait(false);

            if (!_subscribeEvents)
            {
                _refresh.Refresh += OnRefresh;
                _adapter.ItemClick += OnItemClick;
                _btnBuy.Click += OnBuyClick;
                _subscribeEvents = true;
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
            _refresh.Refresh -= OnRefresh;
            _adapter.ItemClick -= OnItemClick;
            _btnBuy.Click -= OnBuyClick;
            _subscribeEvents = false;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main_menu, menu);
            _menu = menu;
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            ExecuteMenuAction(item);
            return base.OnOptionsItemSelected(item);
        }

        private void ExecuteMenuAction(IMenuItem item)
        {
            int id = item.ItemId - 1000;
            var category = ViewModel.Categories.FirstOrDefault(c => c.Id == id);
            LoadItemsAsync(category);
        }

        private void OnBuyClick(object sender, System.EventArgs e)
        {
            var intent = new Intent(this, typeof(CartActivity));
            StartActivity(intent);
        }

        private async void OnRefresh(object sender, System.EventArgs e)
        {
            await OnRefreshAsync();
        }

        private void OnItemClick(object sender, Helpers.RecyclerClickEventArgs e)
        {
            var item = ViewModel.Items[e.Position];
            if (item.Type == Enums.EGroupItemType.Header)
                return;

            switch (e.View.Id)
            {
                case Resource.Id.imb_favorite:
                    ViewModel.SetFavorite(item.Product);
                    break;
                case Resource.Id.img_remove_item:
                    ViewModel.RemoveProduct(item.Product);
                    break;
                case Resource.Id.imb_add_item:
                    ViewModel.AddProduct(item.Product);
                    break;
            }
            _adapter.NotifyItemChanged(e.Position);
            ShowCartValue();
        }

        private async Task LoadItemsAsync(Category category = null)
        {
            _refresh.Refreshing = true;
            await ViewModel.LoadItemsAsync(category);
            LoadMenu();
            _refresh.Refreshing = false;
        }

        private async Task LoadDataAsync()
        {
            _refresh.Refreshing = true;
            await ViewModel.LoadDataAsync();
            LoadMenu();
            _refresh.Refreshing = false;
            _apiRequest = false;
        }

        async Task OnRefreshAsync()
        {
            await LoadDataAsync().ConfigureAwait(false);
            await LoadItemsAsync().ConfigureAwait(false);
        }

        void ShowCartValue()
        {
            var value = ViewModel.Cart.Total;
            if (value > 0)
            {
                _llCartValue.Visibility = ViewStates.Visible;
                _btnBuy.Text = $"Comprar {value:R$ ###,###,##0.00}";
            }
            else
                _llCartValue.Visibility = ViewStates.Gone;
        }

        void LoadMenu()
        {
            _menu.Clear();
            _menu.Add(0, 1000, 0, "Todas");
            foreach (var item in ViewModel.Categories.OrderBy(c => c.Name))
            {
                _menu.Add(0, 1000 + item.Id, 0, item.Name);
            }
        }
    }
}

