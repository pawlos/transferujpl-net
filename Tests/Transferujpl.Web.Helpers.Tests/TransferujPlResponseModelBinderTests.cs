using Moq;
using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Transferujpl.Web.Helpers.Binders;
using Xunit;

namespace Transferujpl.Web.Helpers.Tests
{
    public class TransferujPlResponseModelBinderTests : Test
    {
        [Fact]
        void BindModelDoesNotThrowException()
        {
            var modelBinder = new TransferujPlResponseModelBinder();
            var controller = MockRepository.Create<ControllerBase>();
            var httpContext = MockHttpRequest(MockRepository, m => m.Setup(x => x.UserHostAddress).Returns("212.12.12.12"));
            var controllerContext = new ControllerContext(httpContext.Object, new RouteData(), controller.Object);
            var modelBindingContext = new ModelBindingContext();
            Assert.DoesNotThrow(() => modelBinder.BindModel(controllerContext, modelBindingContext));
        }

        [Fact]
        void BindModelModelStateIsValidIsFalseWhenIPAddressIsIncorrect()
        {
            var modelBinder = new TransferujPlResponseModelBinder();
            var controller = MockRepository.Create<ControllerBase>();
            var httpContext = MockHttpRequest(MockRepository, m => m.Setup(x => x.UserHostAddress).Returns("212.12.12.12"));
            var controllerContext = new ControllerContext(httpContext.Object, new RouteData(), controller.Object);
            var modelBindingContext = new ModelBindingContext();
            modelBinder.BindModel(controllerContext, modelBindingContext);
            Assert.False(modelBindingContext.ModelState.IsValid);
        }

        [Fact]
        void BindModelModelStateIsValidIsTrueWhenIPAddressIsIncorrect()
        {
            var modelBinder = new TransferujPlResponseModelBinder();
            var controller = MockRepository.Create<ControllerBase>();
            var httpContext = MockHttpRequest(MockRepository, m =>
            {
                m.Setup(x => x.UserHostAddress).Returns("195.149.229.109");
                m.Setup(x => x.Form).Returns(new NameValueCollection());
            });
            var controllerContext = new ControllerContext(httpContext.Object, new RouteData(), controller.Object);
            var modelBindingContext = new ModelBindingContext();
            modelBinder.BindModel(controllerContext, modelBindingContext);
            Assert.True(modelBindingContext.ModelState.IsValid);
        }
    }
}
