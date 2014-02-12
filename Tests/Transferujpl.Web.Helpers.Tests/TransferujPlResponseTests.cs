using Common.Logging;
using Moq;
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

        [Fact]
        void TransferujPlResponseFromNameValueCollectionDoesLogInformationWhenDebugIsEnabled()
        {
            var items = new NameValueCollection { { "id", "123" }, { "tr_id", "3333" } };
            

            var mockRepository = new MockRepository(MockBehavior.Strict);

            var adapter = mockRepository.Create<ILoggerFactoryAdapter>();
            var log = mockRepository.Create<ILog>();
            log.Setup(x => x.IsDebugEnabled).Returns(true);
            log.Setup(x => x.Info("FromNameValueCollection started")).Verifiable();
            log.Setup(x => x.Info("FromNameValueCollection ended")).Verifiable();
            log.Setup(x => x.DebugFormat(It.IsAny<string>(), new object[] { It.IsAny<object>(), It.IsAny<object>() })).Verifiable();
            adapter.Setup(x => x.GetLogger(It.IsAny<Type>())).Returns(log.Object);
            LogManager.Adapter = adapter.Object;

            var response = TransferujPlResponse.FromNameValueCollection(items);
            log.Verify();
        }
    }
}
