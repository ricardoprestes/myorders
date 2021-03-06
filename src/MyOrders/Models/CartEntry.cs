﻿namespace MyOrders.Models
{
    public class CartEntry
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal Discount { get; set; }

        public decimal Subtotal
        {
            get
            {
                var result = 0m;
                if (Product is not null)
                {
                    result = Product.Price * Amount;
                    var discountValue = 0m;
                    if (Discount > 0)
                        discountValue = result * Discount / 100;
                    result -= discountValue;
                }
                return result;
            }
        }
    }
}
