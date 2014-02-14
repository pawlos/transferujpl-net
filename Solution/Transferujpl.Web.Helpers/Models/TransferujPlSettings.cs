using Transferujpl.Web.Helpers.Attributes;

namespace Transferujpl.Web.Helpers.Settings
{
    public class TransferujPlSettings
    {
        //transaction
        [RenderToFormAs("kwota")]
        public decimal Amount { get; set; }
        [RenderToFormAs("opis")]
        public string Description { get; set; }
        [RenderToFormAs("crc")]
        public string Crc { get; set; }
        [RenderToFormAs("md5sum")]
        public string Md5 { get; set; }

        //Seller
        [RenderToFormAs("id")]
        public int SellerId { get; set; }
        [RenderToFormAs("opis_sprzed")]
        public string SellerDescription {get;set;}

        //Notifications
        [RenderToFormAs("wyn_url")]
        public dynamic NotificationUrl { get; set; }
        [RenderToFormAs("wyn_email")]
        public string NotificationEmail { get; set; }
        [RenderToFormAs("pow_url")]
        public object SuccessReturnUrl { get; set; }
        [RenderToFormAs("pow_url_blad")]
        public object FailureReturnUrl { get; set; }

        //Buyer
        [RenderToFormAs("imie")]
        public string BuyerFirstName { get; set; }
        [RenderToFormAs("nazwisko")]
        public string BuyerLastName { get; set; }
        [RenderToFormAs("email")]
        public string BuyerEmail { get; set; }
        [RenderToFormAs("adres")]
        public string BuyerAddress { get; set; }
        [RenderToFormAs("miasto")]
        public string BuyerCity { get; set; }
        [RenderToFormAs("kod")]
        public string BuyerPostalCode { get; set; }
        [RenderToFormAs("telefon")]
        public string BuyerPhoneNumber { get; set; }
        [RenderToFormAs("kraj")]
        public string BuyerCountry { get; set; }
        [RenderToFormAs("jezyk")]
        public Language Language { get; set; }

        //Misc
        [RenderToFormAs("online")]
        public bool IsOnline { get; set; }
        [RenderToFormAs("kanal")]
        public int Channel { get; set; }
        [RenderToFormAs("zablokuj")]
        public bool LockChannel { get; set; }
        public bool SendIntegrityVerification { get; set; }
        public string SellerSecret { get; set; }
    }
}
