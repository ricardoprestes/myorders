using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyOrders.Helpers;
using MyOrders.Services.Abstractions;

namespace MyOrders.Services
{
    public class ProductService : IProductService
    {
        readonly IApiService _apiService;

        public ProductService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public Task<List<GroupItem>> GetGroupedProducts()
        {
            throw new NotImplementedException();
        }
    }
}
