using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using MyOrders.Helpers;
using MyOrders.Models;
using MyOrders.Services.Abstractions;

namespace MyOrders.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly IApiService _apiService;
        private readonly IProductService _productService;

        public Cart Cart { get; set; }
        public ObservableCollection<GroupItem> Items { get; set; }

        public MainViewModel(IApiService apiService,
                             IProductService productService)
        {
            _apiService = apiService;
            _productService = productService;
            Cart = new Cart();
            Items = new ObservableCollection<GroupItem>();
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
                var items = await _productService.GetGroupedProducts(sales, products);
                foreach (var item in items)
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

        public void AddProduct(Product product)
        {
            var item = Items.FirstOrDefault(i => i.Type == Enums.EGroupItemType.Product && i.Product.Id == product.Id);
            if (item is null)
                return;

            item.Count++;
        }

        public void RemoveProduct(Product product)
        {
            var item = Items.FirstOrDefault(i => i.Type == Enums.EGroupItemType.Product && i.Product.Id == product.Id);
            if (item is null)
                return;

            if (item.Count > 0)
                item.Count--;
        }
    }
}
