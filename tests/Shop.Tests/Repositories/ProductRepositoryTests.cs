using AutoFixture;
using FluentAssertions;
using Shop.Core.Domain;
using Shop.Core.Repositories;
using Xunit;

namespace Shop.Tests.Repositories
{
    public class ProductRepositoryTests
    {
        [Fact]
        public void adding_the_product_should_succeed()
        {
            //Arrange
            IProductRepository productRepository = new ProductRepository();
            var fixture = new Fixture();
            var product = fixture.Create<Product>();

            //Act
            productRepository.Add(product);
            
            //Assert
            var expectedProduct = productRepository.Get(product.Id);
            expectedProduct.ShouldBeEquivalentTo(product);

            var products = productRepository.GetAll();
            products.Should().NotBeEmpty();
            products.Should().Contain(p => p.Id == product.Id);
        }

        public void update_a_product_should_succeed()
        {
            IProductRepository productRepository = new ProductRepository();
            var fixture = new Fixture();
            var product = fixture.Create<Product>();

            //Act
            productRepository.Update(product);

            //Assert
            var expectedProduct = productRepository.Get(product.Id);
            expectedProduct.ShouldBeEquivalentTo(product);

            var products = productRepository.GetAll();
            products.Should().NotBeEmpty();
            products.Should().Contain(p => p.Id == product.Id);
        }
    }
}
