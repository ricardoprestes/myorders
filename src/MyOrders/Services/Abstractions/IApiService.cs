using System.Collections.Generic;
using System.Threading.Tasks;
using MyOrders.Models;

namespace MyOrders.Services.Abstractions
{
    public interface IApiService
    {
        Task<List<Category>> GetCategories();
        Task<List<Sale>> GetSales();
        Task<List<Product>> GetProducts();
    }
}
