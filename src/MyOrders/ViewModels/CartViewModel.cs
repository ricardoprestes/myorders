using System;
using System.Collections.ObjectModel;
using MyOrders.Models;

namespace MyOrders.ViewModels
{
    public class CartViewModel : BaseViewModel
    {
        public ObservableCollection<CartEntry> Items { get; set; }

        public CartViewModel()
        {
            Title = "Carrinho";
            Items = new ObservableCollection<CartEntry>();
        }

        public void LoadItems()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                foreach (var item in App.Cart.Entries)
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
    }
}
