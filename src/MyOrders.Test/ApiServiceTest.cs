using FluentAssertions;
using MyOrders.Services;
using Xunit;

namespace MyOrders.Test
{
    public class ApiServiceTest
    {
        private readonly ApiService _apiService;

        public ApiServiceTest()
        {
            App.StartUp();
            _apiService = new ApiService();
        }

        [Fact]
        public async void TestGetCategories()
        {
            var categories = await _apiService.GetCategories();
            categories.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async void TestGetProducts()
        {
            var products = await _apiService.GetProducts();
            products.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async void TestGetSales()
        {
            var sales = await _apiService.GetSales();
            sales.Should().NotBeNullOrEmpty();
        }
    }
}
