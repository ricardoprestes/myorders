using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using MyOrders.ViewModels;

namespace MyOrders.Droid.Activities
{
    [Activity(Theme = "@style/AppTheme")]
    public class CartActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.activity_cart;
        public CartViewModel ViewModel { get; set; }

        TextView _txvAmount, _txvTotalValue;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = new CartViewModel();
            Toolbar.Title = ViewModel.Title;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);

            _txvAmount = FindViewById<TextView>(Resource.Id.txv_amount);
            _txvTotalValue = FindViewById<TextView>(Resource.Id.txv_total_value);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    Finish();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        protected override void OnStart()
        {
            base.OnStart();
            _txvAmount.Text = $"{App.Cart.Count} UN";
            _txvTotalValue.Text = $"{App.Cart.Total:R$ ###,###,##0.00}";
        }
    }
}
