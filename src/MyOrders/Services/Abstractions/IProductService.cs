using System.Collections.Generic;
using System.Threading.Tasks;
using MyOrders.Helpers;

namespace MyOrders.Services.Abstractions
{
    public interface IProductService
    {
        Task<List<GroupItem>> GetGroupedProducts();
    }
}
