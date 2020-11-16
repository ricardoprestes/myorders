using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyOrders.Models;
using MyOrders.Services.Abstractions;

namespace MyOrders.Services
{
    public class ApiService : IApiService
    {
        public ApiService()
        {
        }

        public Task<List<Category>> GetCategories()
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetProducts()
        {
            throw new NotImplementedException();
        }

        public Task<List<Sale>> GetSales()
        {
            throw new NotImplementedException();
        }
    }
}
