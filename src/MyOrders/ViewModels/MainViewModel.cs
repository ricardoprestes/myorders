using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyOrders.Helpers;
using MyOrders.Services.Abstractions;

namespace MyOrders.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;

        public List<GroupItem> Items { get; set; }

        public MainViewModel(IApiService apiService)
        {
            _apiService = apiService;
            Items = new List<GroupItem>();
            Title = "Catálogo";
        }

        public async Task LoadItemsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var sales = await _apiService.GetSales();
                var products = await _apiService.GetProducts();

                Items.Clear();

                foreach (var sale in sales)
                {
                    var salesProducts = products.Where(p => p.CategoryId == sale.CategoryId);
                    if (salesProducts.Any())
                    {
                        Items.Add(new GroupItem { Type = Enums.EGroupItemType.Header, Sale = sale });
                        foreach (var product in salesProducts)
                        {
                            Items.Add(new GroupItem { Type = Enums.EGroupItemType.Product, Product = product });
                        }
                    }
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
