using FluentAssertions;
using MyOrders.Helpers;
using MyOrders.Models;
using MyOrders.Services.Abstractions;
using Xunit;

namespace MyOrders.Test
{
    public class CartServiceTest
    {
        private readonly ICartService _cartService;

        public CartServiceTest()
        {
            App.Initialize();
            _cartService = ServiceLocator.Instance.Get<ICartService>();
        }

        [Fact]
        public void TestAddProductWithNullParameters()
        {
            Cart cart = null;
            Product product = null;
            _cartService.AddProduct(cart, product);
            cart.Should().BeNull();
        }

        [Fact]
        public void TestGetAddProductWithNullCart()
        {
            Cart cart = null;
            Product product = new Product { Id = 1, CategoryId = 1, Name = "Product 1", Price = 100 };
            _cartService.AddProduct(cart, product);
            cart.Should().BeNull();
        }

        [Fact]
        public void TestGetAddProductWithCartAndProduct()
        {
            Cart cart = new Cart
            {
                Entries = new System.Collections.Generic.List<CartEntry>
                {
                    new CartEntry
                    {
                        ProductId = 1,
                        Amount = 3
                    }
                }
            };
            Product product = new Product { Id = 1, CategoryId = 1, Name = "Product 1", Price = 100 };
            _cartService.AddProduct(cart, product);
            cart.Entries.Should().NotBeNullOrEmpty();
            cart.Entries[0].Amount.Should().Be(4);
        }

        [Fact]
        public void TestRemoveProductWithNullParameters()
        {
            Cart cart = null;
            Product product = null;
            _cartService.RemoveProduct(cart, product);
            cart.Should().BeNull();
        }

        [Fact]
        public void TestRemoveProductWithProductNull()
        {
            Cart cart = new Cart();
            Product product = null;
            _cartService.RemoveProduct(cart, product);
            cart.Should().NotBeNull();
        }

        [Fact]
        public void TestGetRemoveProductWithCartAndProduct()
        {
            Cart cart = new Cart
            {
                Entries = new System.Collections.Generic.List<CartEntry>
                {
                    new CartEntry
                    {
                        ProductId = 1,
                        Amount = 3
                    }
                }
            };
            Product product = new Product { Id = 1, CategoryId = 1, Name = "Product 1", Price = 100 };
            _cartService.RemoveProduct(cart, product);
            cart.Entries.Should().NotBeNullOrEmpty();
            cart.Entries[0].Amount.Should().Be(2);
        }

        [Fact]
        public void TestApplyDiscountWithNullParameter()
        {
            var discount =_cartService.ApplyDiscount(null, null, null);
            discount.Should().Be(0);
        }

        [Fact]
        public void TestApplyDiscountWithNoPolicies()
        {
            var cart = new Cart
            {
                Entries = new System.Collections.Generic.List<CartEntry>
                {
                    new CartEntry
                    {
                        ProductId = 1,
                        Amount = 3
                    }
                }
            };
            var product = new Product { Id = 1, CategoryId = 1, Name = "Product 1", Price = 100 };
            var sale = new Sale { CategoryId = 1, Name = "Sale 1" };

            var discount = _cartService.ApplyDiscount(cart, product, sale);
            discount.Should().Be(0);
        }

        [Fact]
        public void TestApplyDiscountWith()
        {
            var cart = new Cart
            {
                Entries = new System.Collections.Generic.List<CartEntry>
                {
                    new CartEntry
                    {
                        ProductId = 1,
                        Amount = 3
                    }
                }
            };
            var product = new Product { Id = 1, CategoryId = 1, Name = "Product 1", Price = 100 };
            var sale = new Sale
            {
                CategoryId = 1,
                Name = "Sale 1",
                Policies = new System.Collections.Generic.List<Policy>
                {
                    new Policy
                    {
                        Discount = 10,
                        Min = 3
                    }
                }
            };

            var discount = _cartService.ApplyDiscount(cart, product, sale);
            discount.Should().Be(10);
        }
    }
}
