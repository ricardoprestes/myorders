using System;
using System.Linq;
using MyOrders.Helpers;
using MyOrders.Models;
using MyOrders.Services.Abstractions;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace MyOrders.Services
{
    public class CartService : ICartService
    {
        public CartService()
        {
        }

        public void AddProduct(Cart cart, Product product)
        {
            if (cart is null || product is null)
                return;

            if (cart.Entries.Any(e => e.ProductId == product.Id))
            {
                cart.Entries.First(e => e.ProductId == product.Id).Amount++;
            }
            else
            {
                cart.Entries.Add(new CartEntry
                {
                    Product = product,
                    ProductId = product.Id,
                    Amount = 1
                });
            }
        }

        public decimal ApplyDiscount(Cart cart, Product product, Sale sale)
        {
            if (cart is null || product is null || sale is null || sale.Policies is null || !sale.Policies.Any())
                return 0;

            var entry = cart.Entries.FirstOrDefault(e => e.ProductId == product.Id);
            if (entry is null)
                return 0;

            entry.Discount = 0;
            var polices = sale.Policies
                                .Where(p => p.Min <= entry.Amount)
                                .OrderBy(p => p.Min)
                                .ToList();
            if (polices.Any())
            {
                var avalaiblePolicy = polices.Last();
                entry.Discount = avalaiblePolicy.Discount;
                return entry.Discount;
            }

            return 0;
        }

        public void ClearCart()
        {
            Preferences.Remove(Constants.CART);
        }

        public Cart GetCart()
        {
            try
            {
                if (Preferences.ContainsKey(Constants.CART))
                    return JsonConvert.DeserializeObject<Cart>(Preferences.Get(Constants.CART, string.Empty));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return new Cart();
        }

        public void RemoveProduct(Cart cart, Product product)
        {
            if (cart is null || product is null)
                return;

            var entry = cart.Entries.FirstOrDefault(e => e.ProductId == product.Id);
            if (entry is null)
                return;

            if (entry.Amount == 1)
            {
                cart.Entries.Remove(entry);
            }
            else
            {
                cart.Entries.First(e => e.ProductId == product.Id).Amount--;
            }
        }
    }
}
