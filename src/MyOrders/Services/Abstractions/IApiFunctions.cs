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
        [Get("/YNR2rsWe")]
        Task<List<Category>> GetCategories();
        [Get("/eVqp7pfX")]
        Task<List<Sale>> GetSales();
        [Get("/R9cJFBtG")]
        Task<List<Product>> GetProducts();
    }
}
