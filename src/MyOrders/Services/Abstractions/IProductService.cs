using System.Collections.Generic;
using System.Threading.Tasks;
using MyOrders.Helpers;
using MyOrders.Models;

namespace MyOrders.Services.Abstractions
{
    public interface IProductService
    {
        Task<IEnumerable<GroupItem>> GetGroupedProducts(List<Sale> sales, List<Product> products);
    }
}
