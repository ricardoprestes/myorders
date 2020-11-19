using System;
using FFImageLoading;
using MyOrders.Enums;
using MyOrders.Helpers;
using UIKit;

namespace MyOrders.iOS
{
    public partial class SaleProductCell : UITableViewCell
    {
        public SaleProductCell (IntPtr handle) : base (handle)
        {
        }

        public void SetValue(GroupItem item)
        {
            SetVisibility(item.Type == EGroupItemType.Header);

            if (item.Type == EGroupItemType.Header)
            {
                LblSale.Text = item.Sale.Name;
            }
            else
            {
                LblProductName.Text = item.Product.Name;
                LblProductPrice.Text = $"R$ {item.Product.Price:###,###,###,##0.00}";
                LblAmount.Text = $"{item.Count} UN";
                if (item.Discount > 0)
                {
                    LblDiscount.Text = $"{item.Discount:##0.0}%";
                    VwDiscount.Hidden = false;
                }
                else
                    VwDiscount.Hidden = true;

                if (item.Product.Favorite)
                {
                    BtnFavorite.SetImage(UIImage.GetSystemImage("star.fill"), UIControlState.Normal);
                }
                else
                    BtnFavorite.SetImage(UIImage.GetSystemImage("star"), UIControlState.Normal);

                ImageService.Instance.LoadUrl(item.Product.Photo)
                            //.ErrorPlaceholder("error.png", ImageSource.ApplicationBundle)
                            //.LoadingPlaceholder("placeholder", ImageSource.CompiledResource)
                            .Into(ImgProduct);
            }
        }

        void SetVisibility(bool isHeader)
        {
            LblSale.Hidden = !isHeader;
            ImgProduct.Hidden = isHeader;
            LblProductName.Hidden = isHeader;
            VwDiscount.Hidden = isHeader;
            LblProductPrice.Hidden = isHeader;
            BtnFavorite.Hidden = isHeader;
            LblAmount.Hidden = isHeader;
            BtnAdd.Hidden = isHeader;
            BtnRemove.Hidden = isHeader;
        }
    }
}