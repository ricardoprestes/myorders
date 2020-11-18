using System;
using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MyOrders.Droid.Adapters;
using MyOrders.Helpers;
using MyOrders.Services.Abstractions;
using MyOrders.ViewModels;

namespace MyOrders.Droid.Activities
{
    [Activity(Theme = "@style/AppTheme")]
    public class CartActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.activity_cart;
        public CartViewModel ViewModel { get; set; }

        TextView _txvAmount, _txvTotalValue;
        Button _btnFinishOrder;
        bool _subscribeEvents = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var cartService = ServiceLocator.Instance.Get<ICartService>();
            ViewModel = new CartViewModel(cartService);
            Toolbar.Title = ViewModel.Title;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            _txvAmount = FindViewById<TextView>(Resource.Id.txv_amount);
            _txvTotalValue = FindViewById<TextView>(Resource.Id.txv_total_value);
            _btnFinishOrder = FindViewById<Button>(Resource.Id.btn_buy);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.rv_items);
            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(new CartEntryAdapter(this, ViewModel));
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    Finish();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnStart()
        {
            base.OnStart();
            _txvAmount.Text = $"{ViewModel.Cart.Count} UN";
            _txvTotalValue.Text = $"{ViewModel.Cart.Total:R$ ###,###,##0.00}";
            ViewModel.LoadItems();
            if (!_subscribeEvents)
            {
                _btnFinishOrder.Click += OnClick;
                _subscribeEvents = true;
            }
        }

        protected override void OnStop()
        {
            base.OnStop();
            _btnFinishOrder.Click -= OnClick;
            _subscribeEvents = false;
        }

        private void OnClick(object sender, EventArgs e)
        {
            ShowMessage();
        }

        private void ShowMessage()
        {
            var alert = new AlertDialog.Builder(this);
            alert.SetTitle("Pedido finalizado");
            alert.SetMessage("Seu pedido foi finalizado com sucesso!");

            alert.SetPositiveButton("Ok", (senderAlert, args) =>
            {
                FinishOrder();
            });

            var dialog = alert.Create();
            dialog.Show();
        }

        private void FinishOrder()
        {
            ViewModel.ClearCart();
            Finish();
        }
    }
}
