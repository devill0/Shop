using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop.Web.Controllers;
using Shop.Web.Models;
using Xunit;

namespace Shop.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void index_method_should_return_view()
        {
            //Arrange
            var controller = new HomeController();

            //Act
            var result = controller.Index();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public void error_method_should_return_view_with_view_model()
        {
            //Arrange
            var controller = new HomeController();
            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            //Act
            var result = controller.Error() as ViewResult;

            //Assert
            result.Should().NotBeNull();
            var viewModel = result.Model as ErrorViewModel;
            viewModel.Should().NotBeNull();
        }
    }
}
