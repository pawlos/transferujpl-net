using Transferujpl.Web.Helpers.Attributes;
using Xunit;

namespace Transferujpl.Web.Helpers.Tests
{
    public class RenderToFromAsAttributeTests
    {
        [Fact]
        void RenderToFromAsAttributeDoesSaveNamePassedToContructor()
        {
            var renderToFromAsAttribute = new RenderToFormAsAttribute("Name passed to ctor");

            Assert.Equal("Name passed to ctor", renderToFromAsAttribute.Name);
        }
    }
}
