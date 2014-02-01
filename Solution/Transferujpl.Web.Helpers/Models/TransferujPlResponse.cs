using System;
using Transferujpl.Web.Helpers.Attributes;

namespace Transferujpl.Web.Helpers.Models
{
    public class TransferujPlResponse
    {
        [MapFrom("id")]
        public int SellerId { get; set; }
        [MapFrom("tr_status")]
        public bool Result { get; set; }
        [MapFrom("tr_id")]
        public string TransactionId { get; set; }
        [MapFrom("tr_amount")]
        public decimal Amount { get; set; }
        [MapFrom("tr_paid")]
        public decimal PaidAmount { get; set; }
        [MapFrom("tr_error")]
        public string Error { get; set; }
        [MapFrom("tr_date")]
        public DateTime TransactionDate { get; set; }
        [MapFrom("tr_desc")]
        public string Description { get; set; }
        [MapFrom("tr_crc")]
        public string Crc { get; set; }
        [MapFrom("tr_email")]
        public string BuyerEmail { get; set; }
        [MapFrom("md5sum")]
        public string Md5Sum { get; set; }
    }
}
