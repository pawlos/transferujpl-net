namespace Transferujpl.Web.Helpers.Tests
{
    using Moq;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;
    using Transferujpl.Web.Helpers.Settings;
    using Xunit;
    using Xunit.Extensions;

    public class TransferujPlFormTests : Test
    {
        [Fact]
        void TransferujPlFormThrowsArgumentNullExceptionWhenSettingsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => MvcHelper.TransferujPlForm<object>(null, null));
        }

        [Fact]
        void TransferujPlFormThrowsArgumentExceptionWhenSellerIdIsNotSet()
        {
            var settings = new TransferujPlSettings { SellerId = 0 };
            Assert.Throws<ArgumentException>(() => MvcHelper.TransferujPlForm<object>(null, settings));
        }

        [Fact]
        void TransferujPlFormThrowsArgumentExceptionWhenValueIsNotSet()
        {
            var settings = new TransferujPlSettings { SellerId = 123 };
            Assert.Throws<ArgumentException>(() => MvcHelper.TransferujPlForm<object>(null, settings));
        }

        [Fact]
        void TransferujPlFormThrowsArgumentExceptionWhenDescriptionIsNotSet()
        {
            var settings = new TransferujPlSettings { SellerId = 123, Amount = 100 };
            Assert.Throws<ArgumentException>(() => MvcHelper.TransferujPlForm<object>(null, settings));
        }

        [Fact]
        void TransferujPlFormThrowsArgumentExceptionWhenDescriptionIsSetToWhitespace()
        {
            var settings = new TransferujPlSettings { SellerId = 123, Amount = 100, Description = "\t\n \v" };
            Assert.Throws<ArgumentException>(() => MvcHelper.TransferujPlForm<object>(null, settings));
        }

        [Fact]
        void TransferujPlFormRendersWithActionSetCorrectly()
        {
            var settings = new TransferujPlSettings { SellerId = 123, Amount = 100, Description = "Demo" };
            var generatedForm = MvcHelper.TransferujPlForm<object>(null, settings).ToString();
            Assert.Contains(@"action=""https://secure.transferuj.pl""", generatedForm);
        }

        [Fact]
        void TransferujPlFormRendersWithMethodPost()
        {
            var settings = new TransferujPlSettings { SellerId = 123, Amount = 100, Description = "Demo" };
            var generatedForm = MvcHelper.TransferujPlForm<object>(null, settings).ToString();
            Assert.Contains(@"method=""post""", generatedForm);
        }

        [Fact]
        void TransferujPlFormRendersForm()
        {
            var settings = new TransferujPlSettings { SellerId = 123, Amount = 100, Description = "Demo" };
            var generatedForm = MvcHelper.TransferujPlForm<object>(null, settings).ToString();
            var formIndex = generatedForm.IndexOf("<form");
            var closingFormIndex = generatedForm.IndexOf("</form>");
            Assert.True(closingFormIndex > formIndex); //closing tag exists and is after opening one
        }

        [Fact]
        void TransferujPlFoomRendersWithSellerId()
        {
            var settings = new TransferujPlSettings { SellerId = 324, Amount = 100, Description = "Demo" };
            var generatedForm = MvcHelper.TransferujPlForm<object>(null, settings).ToString();
            Assert.Contains(@"<input name=""id"" type=""hidden"" value=""324"" />", generatedForm);
        }

        [Fact]
        void TransferujPlFormDoesNotRenderFieldThatDoesNotHaveValue()
        {
            var settings = new TransferujPlSettings { SellerId = 333, Amount = 100, Description = "Demo" };
            var generatedForm = MvcHelper.TransferujPlForm<object>(null, settings).ToString();
            Assert.DoesNotContain(@"<input name=""email"" type=""hidden"" value="""" />", generatedForm);
        }

        [Fact]
        void TransferujPlFormDoesRenderFieldThatHasValue()
        {
            var settings = new TransferujPlSettings { SellerId = 456, Amount = 100, Description = "Demo", BuyerEmail = "test@test.pl" };
            var generatedForm = MvcHelper.TransferujPlForm<object>(null, settings).ToString();
            Assert.Contains(@"<input name=""email"" type=""hidden"" value=""test@test.pl"" />", generatedForm);
        }

        [Theory]
        [InlineData("BuyerAddress")]
        [InlineData("BuyerEmail")]
        [InlineData("BuyerFirstName")]
        [InlineData("BuyerLastName")]
        [InlineData("BuyerCity")]
        [InlineData("BuyerPostalCode")]
        [InlineData("BuyerPhoneNumber")]
        [InlineData("BuyerCountry")]
        void TransferujPlFormDoesRenderBuyerFields(string propertyName)
        {
            var dict = new Dictionary<string, string> {
                {"BuyerAddress","adres"},
                {"BuyerEmail", "email"},
                {"BuyerFirstName","imie"},
                {"BuyerLastName","nazwisko"},
                {"BuyerCity","miasto"},
                {"BuyerPostalCode","kod"},
                {"BuyerPhoneNumber","telefon"},
                {"BuyerCountry","kraj"}
            };
            var settings = new TransferujPlSettings { SellerId = 1234, Amount = 100, Description = "Demo" };
            var property = settings.GetType().GetProperty(propertyName);
            property.SetValue(settings, propertyName);
            
            var generatedForm = MvcHelper.TransferujPlForm<object>(null, settings).ToString();
            Assert.Contains(string.Format(@"<input name=""{1}"" type=""hidden"" value=""{0}"" />", propertyName, dict[propertyName]), generatedForm);
        }

        [Fact]
        void TransferujPlFormThrowsArgumentExceptionWhenSendIntegrityVerificationIsSetAndSellerSecretIsNull()
        {
            var settings = new TransferujPlSettings { SellerId = 123, Amount = 100, Description = "Demo" };
            settings.SendIntegrityVerification = true;

            Assert.Throws<ArgumentException>(() => MvcHelper.TransferujPlForm<object>(null, settings));
        }

        [Fact]
        void TransferujPlFormRendersMd5SumTagWhenSendIntegrityVerificationIsSet()
        {
            var settings = new TransferujPlSettings { SellerId = 123, Amount = 100, Description = "Demo" };
            settings.SendIntegrityVerification = true;
            settings.SellerSecret = "secret";
            var generatedForm = MvcHelper.TransferujPlForm<object>(null, settings).ToString();
            Assert.Contains(@"<input name=""md5sum""", generatedForm);
        }

        [Fact]
        void TransferujPlFormRendersMd5SumTagWithMd5ValueOfIdValueCrcSecret()
        {
            var settings = new TransferujPlSettings() { SellerId = 123, Amount = 100, Description = "Demo" };
            settings.SellerSecret = "Secret";
            settings.SendIntegrityVerification = true;

            var generatedForm = MvcHelper.TransferujPlForm<object>(null, settings).ToString();
            Assert.Contains(@"<input name=""md5sum"" type=""hidden"" value=""4F2957086AAA1AC69812574393D2A4A0"" />", generatedForm);
        }

        [Fact]
        void TransferujPlFormMd5SumTagIsDependentOnValue()
        {
            var settings = new TransferujPlSettings() { SellerId = 123, Amount = 99, Description = "Demo" };
            settings.SellerSecret = "Secret";
            settings.SendIntegrityVerification = true;

            var generatedForm = MvcHelper.TransferujPlForm<object>(null, settings).ToString();
            Assert.DoesNotContain(@"4F2957086AAA1AC69812574393D2A4A0", generatedForm);
        }

        [Fact]
        void TransferujPlFormMd5SumTagIsDependentOnSellerSecret()
        {
            var settings = new TransferujPlSettings() { SellerId = 123, Amount = 99, Description = "Demo" };
            settings.SellerSecret = "Secret2";
            settings.SendIntegrityVerification = true;

            var generatedForm = MvcHelper.TransferujPlForm<object>(null, settings).ToString();
            Assert.DoesNotContain(@"4F2957086AAA1AC69812574393D2A4A0", generatedForm);
        }

        [Fact]
        void TransferujPlFormSuccessReturnUrlGeneratesCorrectUrl()
        {
            var settings = new TransferujPlSettings()
            {
                SellerId = 123,
                Amount = 99,
                Description = "Demo",
                SuccessReturnUrl = new { controller = "Demo", action = "Success" }
            };

            var htmlHelper = CreateHtmlHelper<object>(new ViewDataDictionary());
            var generatedForm = htmlHelper.TransferujPlForm<object>(settings).ToString();
            Assert.Contains(@"<input name=""pow_url"" type=""hidden"" value=""http://jakis.adres/Demo/Success""", generatedForm);
        }

        [Fact]
        void TransferujPlFormFailureUrlGeneratesCorrectUrl()
        {
            var settings = new TransferujPlSettings()
            {
                SellerId = 123,
                Amount = 99,
                Description = "Demo",
                FailureReturnUrl = new { controller = "Demo", action = "Failure" }
            };

            var htmlHelper = CreateHtmlHelper<object>(new ViewDataDictionary());
            var generatedForm = htmlHelper.TransferujPlForm<object>(settings).ToString();
            Assert.Contains(@"<input name=""pow_url_blad"" type=""hidden"" value=""http://jakis.adres/Demo/Failure"" />", generatedForm);
        }

        [Fact]
        void TransferujPlFormNotificationUrlGeneratedCorrectUrl()
        {
            var settings = new TransferujPlSettings()
            {
                SellerId = 123,
                Amount = 99,
                Description = "Demo",
                NotificationUrl = new { controller = "Demo", action = "Notification"}
            };

            var htmlHelper = CreateHtmlHelper<object>(new ViewDataDictionary());
            var generatedForm = htmlHelper.TransferujPlForm<object>(settings).ToString();
            Assert.Contains(@"<input name=""wyn_url"" type=""hidden"" value=""http://jakis.adres/Demo/Notification"" />", generatedForm);
        }

        [Fact]
        void TransferujPlFormGeneratesCorrectOnlineTagWhenIsOnlineSetToTrue()
        {
            var settings = new TransferujPlSettings()
            {
                SellerId = 123,
                Amount = 99,
                Description = "Demo payment",
                IsOnline = true
            };
            var generatedForm = MvcHelper.TransferujPlForm<object>(null, settings).ToString();
            Assert.Contains(@"<input name=""online"" type=""hidden"" value=""1"" />", generatedForm);
        }

        [Fact]
        void TransferujPlFormGeneratesCorrectOnlineTagWhenIsOnlineSetToFalse()
        {
            var settings = new TransferujPlSettings()
            {
                SellerId = 123,
                Amount = 99,
                Description = "Demo payment",
                IsOnline = false
            };
            var generatedForm = MvcHelper.TransferujPlForm<object>(null, settings).ToString();
            Assert.Contains(@"<input name=""online"" type=""hidden"" value=""0"" />", generatedForm);
        }

    }
}
