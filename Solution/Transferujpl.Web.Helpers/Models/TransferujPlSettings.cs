using Transferujpl.Web.Helpers.Attributes;

namespace Transferujpl.Web.Helpers.Settings
{
    public class TransferujPlSettings
    {
        //transaction
        [RenderToFormAsAttribute("kwota")]
        public decimal Amount { get; set; }
        [RenderToFormAsAttribute("opis")]
        public string Description { get; set; }
        [RenderToFormAsAttribute("crc")]
        public string Crc { get; set; }
        [RenderToFormAsAttribute("md5sum")]
        public string Md5 { get; set; }

        //Seller
        [RenderToFormAsAttribute("id")]
        public int SellerId { get; set; }
        [RenderToFormAsAttribute("opis_sprzed")]
        public string SellerDescription {get;set;}

        //Notifications
        [RenderToFormAsAttribute("wyn_url")]
        public dynamic NotificationUrl { get; set; }
        [RenderToFormAsAttribute("wyn_email")]
        public string NotificationEmail { get; set; }
        [RenderToFormAsAttribute("pow_url")]
        public dynamic SuccessReturnUrl { get; set; }
        [RenderToFormAsAttribute("pow_url_blad")]
        public dynamic FailureReturnUrl { get; set; }

        //Buyer
        [RenderToFormAsAttribute("imie")]
        public string BuyerFirstName { get; set; }
        [RenderToFormAsAttribute("nazwisko")]
        public string BuyerLastName { get; set; }
        [RenderToFormAsAttribute("email")]
        public string BuyerEmail { get; set; }
        [RenderToFormAsAttribute("adres")]
        public string BuyerAddress { get; set; }
        [RenderToFormAsAttribute("miasto")]
        public string BuyerCity { get; set; }
        [RenderToFormAsAttribute("kod")]
        public string BuyerPostalCode { get; set; }
        [RenderToFormAsAttribute("telefon")]
        public string BuyerPhoneNumber { get; set; }
        [RenderToFormAsAttribute("kraj")]
        public string BuyerCountry { get; set; }
        [RenderToFormAsAttribute("jezyk")]
        public Language Language { get; set; }

        public bool SendIntegrityVerification { get; set; }
        public string SellerSecret { get; set; }
    }
}
