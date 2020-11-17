using FluentAssertions;
using MyOrders.Helpers;
using MyOrders.Services.Abstractions;
using Xunit;

namespace MyOrders.Test
{
    public class ApiServiceTest
    {
        private readonly IApiService _apiService;

        public ApiServiceTest()
        {
            App.Initialize();
            _apiService = ServiceLocator.Instance.Get<IApiService>();
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
