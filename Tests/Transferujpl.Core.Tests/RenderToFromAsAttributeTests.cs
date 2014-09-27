using Transferujpl.Core;
using Xunit;

namespace Transferujpl.Core.Tests
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
