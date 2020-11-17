using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyOrders.Helpers;
using MyOrders.Models;
using MyOrders.Services.Abstractions;

namespace MyOrders.Services
{
    public class ProductService : IProductService
    {
        public ProductService()
        {
        }

        public async Task<IEnumerable<GroupItem>> GetGroupedProducts(List<Sale> sales, List<Product> products)
        {
            if (sales is null || products is null)
                return null;

            if (!sales.Any() || !products.Any())
                return null;

            var items = new List<GroupItem>();
            await Task.Run(() =>
            {
                foreach (var sale in sales)
                {
                    var salesProducts = products.Where(p => p.CategoryId.HasValue && p.CategoryId.Value == sale.CategoryId);
                    if (salesProducts.Any())
                    {
                        items.Add(new GroupItem { Type = Enums.EGroupItemType.Header, Sale = sale });
                        foreach (var product in salesProducts)
                        {
                            items.Add(new GroupItem { Type = Enums.EGroupItemType.Product, Product = product });
                        }
                    }
                }
            });
            return items;
        }
    }
}
