using System;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading;
using MyOrders.Droid.Helpers;
using MyOrders.ViewModels;

namespace MyOrders.Droid.Adapters
{
    public class CartEntryAdapter : BaseRecycleViewAdapter
    {
        Activity _activity;
        CartViewModel _viewModel;

        public CartEntryAdapter(Activity activity, CartViewModel viewModel)
        {
            _activity = activity;
            _viewModel = viewModel;

            _viewModel.Items.CollectionChanged += (sender, args) =>
            {
                _activity.RunOnUiThread(NotifyDataSetChanged);
            };
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = null;
            var id = Resource.Layout.item_cart;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            var vh = new CartEntryViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _viewModel.Items[position];
            var vh = holder as CartEntryViewHolder;
            ImageService.Instance.LoadUrl(item.Product.Photo)
                   .Retry(3, 200)
                   .DownSample(100, 100)
                   .Into(vh.ImgItem);

            vh.TxvProductName.Text = item.Product.Name;
            vh.TxvProductPrice.Text = item.Product.Price.ToString("R$ ###,###,###,##0.00");
            vh.TxvAmount.Text = $"{item.Amount} UN";
            if (item.Discount > 0)
            {
                vh.TxvDiscount.Text = $"{item.Discount:##0.0}%";
                vh.LlDiscount.Visibility = ViewStates.Visible;
            }
            else
                vh.LlDiscount.Visibility = ViewStates.Invisible;
        }

        public override int ItemCount => _viewModel.Items.Count;
    }

    public class CartEntryViewHolder : RecyclerView.ViewHolder
    {
        public ImageView ImgItem { get; set; }
        public TextView TxvProductName { get; set; }
        public TextView TxvAmount { get; set; }
        public TextView TxvProductPrice { get; set; }
        public LinearLayout LlDiscount { get; set; }
        public TextView TxvDiscount { get; set; }

        public CartEntryViewHolder(View itemView, Action<RecyclerClickEventArgs> clickListener,
                            Action<RecyclerClickEventArgs> longClickListener) : base(itemView)
        {

            ImgItem = itemView.FindViewById<ImageView>(Resource.Id.img_item);
            TxvProductName = itemView.FindViewById<TextView>(Resource.Id.txv_product_name);
            TxvAmount = itemView.FindViewById<TextView>(Resource.Id.txv_amount);
            TxvProductPrice = itemView.FindViewById<TextView>(Resource.Id.txv_product_price);
            LlDiscount = itemView.FindViewById<LinearLayout>(Resource.Id.ll_discount);
            TxvDiscount = itemView.FindViewById<TextView>(Resource.Id.txv_discount);

            itemView.Click += (sender, e) => clickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }
}
