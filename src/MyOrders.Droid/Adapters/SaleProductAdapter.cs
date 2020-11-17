﻿using System;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading;
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
            var vh = holder as SaleProductViewHolder;
            if(item.Type == Enums.EGroupItemType.Header)
            {
                vh.TxtHeader.Text = item.Sale.Name;
                vh.TxtHeader.Visibility = ViewStates.Visible;
                vh.CardItem.Visibility = ViewStates.Gone;
            }
            else
            {
                ImageService.Instance.LoadUrl(item.Product.Photo)
                   .Retry(3, 200)
                   .DownSample(100, 100)
                   //.LoadingPlaceholder(Config.LoadingPlaceholderPath, FFImageLoading.Work.ImageSource.ApplicationBundle)
                   //.ErrorPlaceholder(Config.ErrorPlaceholderPath, FFImageLoading.Work.ImageSource.ApplicationBundle)
                   .Into(vh.ImgItem);

                vh.TxvProductName.Text = item.Product.Name;
                vh.TxvProductPrice.Text = item.Product.Price.ToString("R$ ###,###,###,##0.00");
                vh.CardItem.Visibility = ViewStates.Visible;
                vh.TxtHeader.Visibility = ViewStates.Gone;
            }
        }

        public override int ItemCount => _viewModel.Items.Count;
    }

    public class SaleProductViewHolder : RecyclerView.ViewHolder
    {
        public TextView TxtHeader { get; set; }
        public CardView CardItem { get; set; }
        public ImageView ImgItem { get; set; }
        public TextView TxvProductName { get; set; }
        public TextView TxvProductPrice { get; set; }
        public ImageButton ImbFavorite { get; set; }

        public SaleProductViewHolder(View itemView, Action<RecyclerClickEventArgs> clickListener,
                            Action<RecyclerClickEventArgs> longClickListener) : base(itemView)
        {
            TxtHeader = itemView.FindViewById<TextView>(Resource.Id.txv_header);
            CardItem = itemView.FindViewById<CardView>(Resource.Id.card_item);
            TxvProductName = itemView.FindViewById<TextView>(Resource.Id.txv_product_name);
            TxvProductPrice = itemView.FindViewById<TextView>(Resource.Id.txv_product_price);
            ImgItem = itemView.FindViewById<ImageView>(Resource.Id.img_item);
            ImbFavorite = itemView.FindViewById<ImageButton>(Resource.Id.imb_favorite);

            ImbFavorite.Click += (sender, e) => clickListener(new RecyclerClickEventArgs { View = ImbFavorite, Position = AdapterPosition });
            itemView.Click += (sender, e) => clickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }
}