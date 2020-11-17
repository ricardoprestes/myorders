using System;
using System.Collections.Generic;
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
        private readonly ICartService _cartService;

        public Cart Cart { get; set; }
        public ObservableCollection<GroupItem> Items { get; set; }
        public List<Sale> Sales { get; set; }
        public List<Product> Products { get; set; }

        public MainViewModel(IApiService apiService,
                             IProductService productService,
                             ICartService cartService)
        {
            _apiService = apiService;
            _productService = productService;
            _cartService = cartService;

            Cart = new Cart();
            Items = new ObservableCollection<GroupItem>();
            Sales = new List<Sale>();
            Products = new List<Product>();
            Title = "Catálogo";
        }

        public async Task LoadItemsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Sales = await _apiService.GetSales();
                Products = await _apiService.GetProducts();

                Items.Clear();
                var items = await _productService.GetGroupedProducts(Sales, Products);
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
            _cartService.AddProduct(Cart, product);
            var sale = Sales.FirstOrDefault(s => s.CategoryId == product.CategoryId);
            if(sale is not null)
                item.Discount = _cartService.ApplyDiscount(Cart, product, sale);
        }

        public void RemoveProduct(Product product)
        {
            var item = Items.FirstOrDefault(i => i.Type == Enums.EGroupItemType.Product && i.Product.Id == product.Id);
            if (item is null)
                return;

            if (item.Count > 0)
            {
                item.Count--;
                _cartService.RemoveProduct(Cart, product);
                var sale = Sales.FirstOrDefault(s => s.CategoryId == product.CategoryId);
                if (sale is not null)
                    item.Discount = _cartService.ApplyDiscount(Cart, product, sale);
            }
        }
    }
}
