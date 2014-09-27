using Moq;
using System.Web.Mvc;
using System.Web.Routing;
using Transferujpl.Web.Mvc.ActionResults;
using Xunit;

namespace Transferujpl.Web.Mvc.Tests
{
    public class TransferujPlActionResultTests : Test
    {
        [Fact]
        void TransferujPlActionResultWritesYESToResponse()
        {
            var transferujPlActionResult = new TransferujPlActionResult();

            var controller = MockRepository.Create<ControllerBase>();
            var httpContext = MockHttpRequest(MockRepository, null, r =>
            {
                r.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>()));
                r.Setup(x => x.Write("YES")).Verifiable();
            });
            var controllerContext = new ControllerContext(httpContext.Object, new RouteData(), controller.Object);
            transferujPlActionResult.ExecuteResult(controllerContext);

            MockRepository.Verify();
        }
    }
}
