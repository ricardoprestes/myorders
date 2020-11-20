using System;
using FFImageLoading;
using MyOrders.Models;
using UIKit;

namespace MyOrders.iOS
{
    public partial class CartEntryViewCell : UITableViewCell
    {
        public CartEntryViewCell (IntPtr handle) : base (handle)
        {
        }

        public void SetValues(CartEntry cartEntry)
        {
            LblProductName.Text = cartEntry.Product.Name;
            LblProductPrice.Text = $"R$ {cartEntry.Subtotal:###,###,###,##0.00}";
            LblAmount.Text = $"{cartEntry.Amount} UN";
            if (cartEntry.Discount > 0)
            {
                LblDiscount.Text = $"{cartEntry.Discount:##0.0}%";
                VwDiscount.Hidden = false;
            }
            else
                VwDiscount.Hidden = true;

            ImageService.Instance.LoadUrl(cartEntry.Product.Photo)
                        .Into(ImgProduct);
        }
    }
}