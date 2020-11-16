using System.Collections.Generic;
using System.Threading.Tasks;
using MyOrders.Helpers;
using MyOrders.Models;
using Refit;

namespace MyOrders.Services.Abstractions
{
    [Headers("Content-Type: application/json")]
    public interface IApiFunctions
    {
        [Get(Constants.URL_CATEGORY)]
        Task<List<Category>> GetCategories();
        [Get(Constants.URL_SALE)]
        Task<List<Sale>> GetSales();
        [Get(Constants.URL_PRODUCT)]
        Task<List<Product>> GetProducts();
    }
}
