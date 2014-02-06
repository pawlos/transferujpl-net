using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transferujpl.Web.Helpers.Models;
using Xunit;

namespace Transferujpl.Web.Helpers.Tests
{
    public class TransferujPlResponseTests
    {
        [Fact]
        void TransferujPlResponseFromNameValueCollectionDoesNotThrowExceptionWhenItemsIsEmpty()
        {
            var items = new NameValueCollection();
            Assert.DoesNotThrow(() => TransferujPlResponse.FromNameValueCollection(items));
        }

        [Fact]
        void TransferujPlResponseFromNameValueCollectionDoesCorrectlySetSellerId()
        {
            var items = new NameValueCollection { { "id", "123" } };
            var response = TransferujPlResponse.FromNameValueCollection(items);
            Assert.Equal(123, response.SellerId);
        }

        [Fact]
        void TransferujPlResponseFromNameValueCollectionDoesCorrectlySetOKState()
        {
            var items = new NameValueCollection { { "id", "123" }, { "tr_status", "TRUE" } };
            var response = TransferujPlResponse.FromNameValueCollection(items);

            Assert.True(response.Result);
        }

        [Fact]
        void TransferujPlResponseFromNameValueCollectionDoesCorrectlySetErrorState()
        {
            var items = new NameValueCollection { { "id", "123" }, { "tr_status", "FALSE" } };
            var response = TransferujPlResponse.FromNameValueCollection(items);

            Assert.False(response.Result);
        }

        [Fact]
        void TransferujPlResponseFromNameValueCollectionDoesCorrectlySetTransactionId()
        {
            var items = new NameValueCollection { { "id", "123" }, { "tr_id", "345a1" } };
            var response = TransferujPlResponse.FromNameValueCollection(items);

            Assert.Equal("345a1", response.TransactionId);
        }
    }
}
