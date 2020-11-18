using Android.App;
using Android.OS;
using Android.Views;
using MyOrders.ViewModels;

namespace MyOrders.Droid.Activities
{
    [Activity(Theme = "@style/AppTheme")]
    public class CartActivity : BaseActivity
    {
        protected override int LayoutResource => Resource.Layout.activity_cart;
        public CartViewModel ViewModel { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            ViewModel = new CartViewModel();
            Toolbar.Title = ViewModel.Title;
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetHomeButtonEnabled(true);
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
    }
}
