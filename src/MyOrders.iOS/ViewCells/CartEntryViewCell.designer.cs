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
    [Register ("CartEntryViewCell")]
    partial class CartEntryViewCell
    {
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
        UIKit.UIView VwDiscount { get; set; }

        void ReleaseDesignerOutlets ()
        {
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

            if (VwDiscount != null) {
                VwDiscount.Dispose ();
                VwDiscount = null;
            }
        }
    }
}