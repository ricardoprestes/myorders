// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MyOrders.iOS
{
    [Register ("SaleProductCell")]
    partial class SaleProductCell
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnAdd { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnFavorite { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIButton BtnRemove { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIImageView ImgProduct { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblAmount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblDiscount { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblProductName { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblProductPrice { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel LblSale { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UIView VwDiscount { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (BtnAdd != null) {
                BtnAdd.Dispose ();
                BtnAdd = null;
            }

            if (BtnFavorite != null) {
                BtnFavorite.Dispose ();
                BtnFavorite = null;
            }

            if (BtnRemove != null) {
                BtnRemove.Dispose ();
                BtnRemove = null;
            }

            if (ImgProduct != null) {
                ImgProduct.Dispose ();
                ImgProduct = null;
            }

            if (LblAmount != null) {
                LblAmount.Dispose ();
                LblAmount = null;
            }

            if (LblDiscount != null) {
                LblDiscount.Dispose ();
                LblDiscount = null;
            }

            if (LblProductName != null) {
                LblProductName.Dispose ();
                LblProductName = null;
            }

            if (LblProductPrice != null) {
                LblProductPrice.Dispose ();
                LblProductPrice = null;
            }

            if (LblSale != null) {
                LblSale.Dispose ();
                LblSale = null;
            }

            if (VwDiscount != null) {
                VwDiscount.Dispose ();
                VwDiscount = null;
            }
        }
    }
}