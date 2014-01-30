namespace Transferujpl.Web.Helpers
{
    using System.Web.Mvc;
    using Settings;
    using System;
    using System.Linq;
    using Transferujpl.Web.Helpers.Attributes;

    public static class MvcHelper
    {
        public static MvcHtmlString TransferujPlForm<T>(this HtmlHelper<T> helper, TransferujPlSettings setting)
        {
            if (setting == null) throw new ArgumentNullException("settings");

            TagBuilder form = new TagBuilder("form");
            form.Attributes.Add("method", "post");
            form.Attributes.Add("accept-charset","utf-8");
            form.Attributes.Add("action","https://secure.transferuj.pl");

            var properties = setting.GetType().GetProperties();
            foreach(var property in properties.Where(p=>p.CustomAttributes.Any(a=>a.AttributeType == typeof(RenderToFormAs))))
            {
                var renderToFormAs = property.CustomAttributes.First(x=>x.AttributeType == typeof(RenderToFormAs));
                TagBuilder input = new TagBuilder("input");
                input.Attributes.Add("type", "hidden");
                input.Attributes.Add("name", renderToFormAs.ConstructorArguments[0].Value.ToString());
                input.Attributes.Add("value", property.GetValue(setting).ToString());

                form.InnerHtml += input.ToString(TagRenderMode.Normal);
            }
            return new MvcHtmlString(form.ToString());
        }
    }
}
