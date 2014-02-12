using Transferujpl.Web.Helpers.Attributes;
using Xunit;

namespace Transferujpl.Web.Helpers.Tests
{
    public class MapFromAttributeTests
    {
        [Fact]
        void MapFromAttribureDoesSaveNamePassedToCtor()
        {
            var mapFromAttribure = new MapFromAttribute("Name passed to ctor");

            Assert.Equal("Name passed to ctor", mapFromAttribure.Name);
        }
    }
}
