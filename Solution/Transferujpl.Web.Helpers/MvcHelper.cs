namespace Transferujpl.Web.Helpers
{
    using System.Web.Mvc;
    using Settings;
    using System;
    using System.Linq;
    using System.Text;
    using System.Security.Cryptography;
    using System.Reflection;
    using System.Dynamic;

    using Attributes;
    using Extensions;

    public static class MvcHelper
    {
        public static MvcHtmlString TransferujPlForm<T>(this HtmlHelper<T> helper, TransferujPlSettings settings)
        {
            if (settings == null) throw new ArgumentNullException("Settings cannot be null");
            if (settings.SellerId == 0) throw new ArgumentException("SellerId is not set", "SellerId");
            if (settings.Amount == 0) throw new ArgumentException("Value is not set", "Value");
            if (string.IsNullOrWhiteSpace(settings.Description)) throw new ArgumentException("Description is not set", "Description");
            if (settings.SendIntegrityVerification &&
                string.IsNullOrWhiteSpace(settings.SellerSecret)) throw new ArgumentException("SellerSecret cannot be null when SendIntegrityVerification is set to true", "SendIntegrityVerification");

            TagBuilder form = new TagBuilder("form");
            form.Attributes.Add("method", "post");
            form.Attributes.Add("accept-charset","utf-8");
            form.Attributes.Add("action","https://secure.transferuj.pl");

            var properties = settings.GetType().GetProperties();
            foreach(var property in properties.Where(p=>p.CustomAttributes.Any(a=>a.AttributeType == typeof(RenderToFormAsAttribute))))
            {
                if (property.GetValue(settings) == null) continue;

                var renderToFormAs = property.CustomAttributes.First(x=>x.AttributeType == typeof(RenderToFormAsAttribute));
                TagBuilder input = new TagBuilder("input");
                input.Attributes.Add("type", "hidden");
                input.Attributes.Add("name", renderToFormAs.ConstructorArguments[0].Value.ToString());
                input.Attributes.Add("value", GetValue(helper, property, settings));

                form.InnerHtml += input.ToString(TagRenderMode.SelfClosing);
            }
            if (settings.SendIntegrityVerification)
            {
                TagBuilder md5Sum = new TagBuilder("input");
                md5Sum.Attributes.Add("type", "hidden");
                md5Sum.Attributes.Add("name", "md5sum");
                md5Sum.Attributes.Add("value", MD5.Create().ComputeHash(
                                Encoding.UTF8.GetBytes(string.Format("{0}{1}{2}{3}", settings.SellerId, settings.Amount, settings.Crc, settings.SellerSecret)))
                                        .Select(x => x.ToString("X2")).Aggregate((x, y) => x + y));
                form.InnerHtml += md5Sum.ToString(TagRenderMode.SelfClosing);
            }
            TagBuilder submit = new TagBuilder("input");
            submit.Attributes.Add("type","submit");
            submit.Attributes.Add("value", "Wyślij");
            form.InnerHtml += submit.ToString(TagRenderMode.SelfClosing);
            return MvcHtmlString.Create(form.ToString(TagRenderMode.Normal));
        }

        private static string GetValue<T>(HtmlHelper<T> helper, PropertyInfo property, TransferujPlSettings settings)
        {
            var value = property.GetValue(settings);
            var type = value.GetType();
            if (type.IsAnonymousType())
            {
                var urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);
                var controller = type.GetProperty("controller", BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public).GetValue(value);
                var action = type.GetProperty("action", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance).GetValue(value);
                return urlHelper.Action(action.ToString(), controller.ToString(), null, "http");
            }
            return value.ToString();
        }
    }
}
