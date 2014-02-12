using System;
using System.Collections.Specialized;
using Transferujpl.Web.Helpers.Attributes;
using System.Linq;
using Common.Logging;

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

        public static TransferujPlResponse FromNameValueCollection(NameValueCollection items)
        {
            ILog log = LogManager.GetLogger(typeof(TransferujPlResponse));
            log.Info("FromNameValueCollection started");
            var transferujPlResponse = new TransferujPlResponse();
            var properties = typeof(TransferujPlResponse).GetProperties().Where(x => x.CustomAttributes.Any(a => a.AttributeType == typeof(MapFromAttribute)));
            foreach (var property in properties)
            {
                var name = property.CustomAttributes.First().ConstructorArguments[0].Value.ToString();
                if (items.AllKeys.Contains(name))
                {
                    var type = property.PropertyType;
                    if (log.IsDebugEnabled)
                        log.DebugFormat("{0}-{1}", property.Name, items[name]);
                    property.SetValue(transferujPlResponse, Convert.ChangeType(items[name], type));
                }
            }
            log.Info("FromNameValueCollection ended");
            return transferujPlResponse;
        }
    }
}
