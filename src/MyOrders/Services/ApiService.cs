using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using MyOrders.Helpers;
using MyOrders.Models;
using MyOrders.Services.Abstractions;
using Refit;

namespace MyOrders.Services
{
    public class ApiService : IApiService
    {
        readonly IApiFunctions _apiFunctions;

        public ApiService()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(Constants.URL_BASE)
            };
            _apiFunctions = RestService.For<IApiFunctions>(client);
        }

        public async Task<List<Category>> GetCategories()
        {
            try
            {
                var categories = await _apiFunctions.GetCategories();
                return categories;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<List<Product>> GetProducts()
        {
            try
            {
                var products = await _apiFunctions.GetProducts();
                return products;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public async Task<List<Sale>> GetSales()
        {
            try
            {
                var sales = await _apiFunctions.GetSales();
                return sales;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
