using System.Collections.Generic;
using FluentAssertions;
using MyOrders.Helpers;
using MyOrders.Models;
using MyOrders.Services.Abstractions;
using Xunit;

namespace MyOrders.Test
{
    public class ProductServiceTest
    {
        private readonly IProductService _productService;

        public ProductServiceTest()
        {
            App.Initialize();
            _productService = ServiceLocator.Instance.Get<IProductService>();
        }

        [Fact]
        public async void TestGetGroupedProductsWithNullParameters()
        {
            var items = await _productService.GetGroupedProducts(null, null);
            items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async void TestGetGroupedProductsWithSalesNull()
        {
            var products = new List<Product>();
            var items = await _productService.GetGroupedProducts(null, products);
            items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async void TestGetGroupedProductsWithProductsNull()
        {
            var sales = new List<Sale>();
            var items = await _productService.GetGroupedProducts(sales, null);
            items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async void TestGetGroupedProductsWithEmptyParameters()
        {
            var sales = new List<Sale>();
            var products = new List<Product>();
            var items = await _productService.GetGroupedProducts(sales, products);
            items.Should().BeNullOrEmpty();
        }

        [Fact]
        public async void TestGetGroupedProductsWithValidParameters()
        {
            List<Sale> sales = GetSales();
            List<Product> products = GetProcuts();
            var items = await _productService.GetGroupedProducts(sales, products);
            items.Should().NotBeNullOrEmpty();
            items.Should().HaveCount(sales.Count + products.Count);
        }

        List<Product> GetProcuts() =>
            new List<Product>
            {
                new Product
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Product 1",
                    Price = 100
                },
                new Product
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Product 2",
                    Price = 150
                },
                new Product
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Product 3",
                    Price = 150
                }
            };

        List<Sale> GetSales() =>
            new List<Sale>
            {
                new Sale {
                    CategoryId = 1,
                    Name = "Sales 1"
                },
                new Sale {
                    CategoryId = 2,
                    Name = "Sales 2"
                }
            };
    }
}
