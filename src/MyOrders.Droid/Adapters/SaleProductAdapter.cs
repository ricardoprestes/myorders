using System;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MyOrders.Droid.Helpers;
using MyOrders.ViewModels;

namespace MyOrders.Droid.Adapters
{
    public class SaleProductAdapter : BaseRecycleViewAdapter
    {
        Activity _activity;
        MainViewModel _viewModel;

        public SaleProductAdapter(Activity activity, MainViewModel viewModel)
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
            var id = Resource.Layout.item_sale_product;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);
            var vh = new SaleProductViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _viewModel.Items[position];
            var myHolder = holder as SaleProductViewHolder;
            myHolder.TxtHeader.Text = item.Type == Enums.EGroupItemType.Header ? item.Sale.Name : item.Product.Name;
        }

        public override int ItemCount => _viewModel.Items.Count;
    }

    public class SaleProductViewHolder : RecyclerView.ViewHolder
    {
        public TextView TxtHeader { get; set; }

        public SaleProductViewHolder(View itemView, Action<RecyclerClickEventArgs> clickListener,
                            Action<RecyclerClickEventArgs> longClickListener) : base(itemView)
        {
            TxtHeader = itemView.FindViewById<TextView>(Resource.Id.txv_header);
            itemView.Click += (sender, e) => clickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }
}
