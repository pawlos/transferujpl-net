using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Transferujpl.Web.Helpers.Tests
{
    public class Test
    {
        public MockRepository MockRepository { get; private set; }

        public Test()
        {
            MockRepository = new MockRepository(MockBehavior.Strict);
        }

        protected HtmlHelper<T> CreateHtmlHelper<T>(ViewDataDictionary viewDataDictionary)
        {
            var iViewDataContainer = MockRepository.Create<IViewDataContainer>();
            iViewDataContainer.Setup(x => x.ViewData).Returns(viewDataDictionary);
            var routeCollection = new RouteCollection();
            routeCollection.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { id = UrlParameter.Optional }
            );

            var controller = MockRepository.Create<ControllerBase>();
            var httpContext = MockHttpRequest(MockRepository);
            var controllerContext = new ControllerContext(httpContext.Object, new RouteData(), controller.Object);

            var viewContext = new ViewContext(controllerContext, MockRepository.Create<IView>().Object,
                viewDataDictionary, new TempDataDictionary(), new StringWriter());

            return new HtmlHelper<T>(viewContext, iViewDataContainer.Object, routeCollection);
        }

        protected static Mock<HttpContextBase> MockHttpRequest(MockRepository mockRepository, 
                                                               Action<Mock<HttpRequestBase>> mockRequest = null,
                                                               Action<Mock<HttpResponseBase>> mockResponse = null)
        {
            var httpContext = mockRepository.Create<HttpContextBase>();

            var request = mockRepository.Create<HttpRequestBase>();
            request.Setup(x => x.ApplicationPath).Returns("/");
            request.Setup(x => x.Url).Returns(new Uri("http://jakis.adres"));
            httpContext.Setup(x => x.Request).Returns(request.Object);
            if (mockRequest != null)
                mockRequest(request);

            var response = mockRepository.Create<HttpResponseBase>();
            response.Setup(x => x.ApplyAppPathModifier(It.IsAny<string>())).Returns((string x) => x);
            httpContext.Setup(x => x.Response).Returns(response.Object);
            if (mockResponse != null)
                mockResponse(response);

            httpContext.Setup(x => x.GetService(It.IsAny<Type>())).Returns((Type)null);

            return httpContext;
        }
    }
}
