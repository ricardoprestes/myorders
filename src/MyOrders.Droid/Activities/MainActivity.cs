using Android.App;
using Android.Widget;
using Android.OS;
using MyOrders.ViewModels;
using MyOrders.Helpers;
using MyOrders.Services.Abstractions;
using System.Linq;
using System.Threading.Tasks;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using MyOrders.Droid.Adapters;

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

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

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
        }

        protected override async void OnStart()
        {
            base.OnStart();
            ShowCartValue();
            await LoadItemsAsync().ConfigureAwait(false);

            _refresh.Refresh += OnRefresh;
            _adapter.ItemClick += OnItemClick;
        }


        protected override void OnStop()
        {
            base.OnStop();
            _refresh.Refresh -= OnRefresh;
            _adapter.ItemClick -= OnItemClick;
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

        private async Task LoadItemsAsync()
        {
            _refresh.Refreshing = true;
            if (!ViewModel.Items.Any())
                await ViewModel.LoadItemsAsync();
            _refresh.Refreshing = false;
        }

        async Task OnRefreshAsync()
        {
            await LoadItemsAsync().ConfigureAwait(false);
        }

        void ShowCartValue()
        {
            var value = ViewModel.Cart.Total;
            if (value > 0)
            {
                _llCartValue.Visibility = Android.Views.ViewStates.Visible;
                _btnBuy.Text = $"Comprar {value:R$ ###,###,##0.00}";
            }
            else
                _llCartValue.Visibility = Android.Views.ViewStates.Gone;
        }
    }
}

