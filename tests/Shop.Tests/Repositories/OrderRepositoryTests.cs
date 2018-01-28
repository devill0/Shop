using AutoFixture;
using FluentAssertions;
using Shop.Core.Domain;
using Shop.Core.Repositories;
using System.Linq;
using Xunit;

namespace Shop.Tests.Repositories
{
    public class OrderRepositoryTests
    {
        [Fact]
        public void adding_an_order_should_succeed()
        {
            //Arrange
            IOrderRepository orderRepository = new OrderRepository();
            var cart = new Cart();
            cart.AddProduct(new Product("test", "test", 1));
            var order = new Order(new User("test", "test"), cart);

            //Act
            orderRepository.Add(order);

            //Assert
            var expectedOrder = orderRepository.Get(order.Id);
            Assert.Equal(expectedOrder, order);

            var orders = orderRepository.Browse(order.UserId);
            Assert.NotEmpty(orders);
            Assert.Single(orders);
            Assert.Contains(orders, o => o.Id == order.Id); // czy na pewno Browse ma ten sam element jakim jest order
        }

        [Fact]
        public void adding_an_order_should_succeed_v2()
        {
            //Arrange
            IOrderRepository orderRepository = new OrderRepository();
            var fixture = new Fixture();
            var user = fixture.Create<User>();
            var cart = fixture.Create<Cart>();
            var product = fixture.Create<Product>();
            cart.AddProduct(product);
            var order = new Order(user, cart);

            //Act
            orderRepository.Add(order);

            //Assert
            var expectedOrder = orderRepository.Get(order.Id);
            expectedOrder.ShouldBeEquivalentTo(order);
        
            var orders = orderRepository.Browse(order.UserId);
            orders.Should().NotBeEmpty();
            orders.Should().ContainSingle();
            orders.Should().Contain(o => o.Id == order.Id);
            //Assert.Contains(orders, o => o.Id == order.Id); // czy na pewno Browse ma ten sam element jakim jest order
        }
    }
}
