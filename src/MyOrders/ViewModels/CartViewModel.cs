using System;
using System.Collections.ObjectModel;
using MyOrders.Models;
using MyOrders.Services.Abstractions;

namespace MyOrders.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        private readonly ICartService _cartService;

        public ObservableCollection<CartEntry> Items { get; set; }
        public Cart Cart { get; set; }

        public CartViewModel(ICartService cartService)
        {
            _cartService = cartService;

            Title = "Carrinho";
            Items = new ObservableCollection<CartEntry>();
            Cart = _cartService.GetCart();
        }

        public void LoadItems()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                foreach (var item in Cart.Entries)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void ClearCart()
        {
            _cartService.ClearCart();
            Cart = _cartService.GetCart();
        }
    }
}
