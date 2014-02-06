using System.Web.Mvc;
using Transferujpl.Web.Helpers.Models;

namespace Transferujpl.Web.Helpers.Binders
{
    public class TransferujPlResponseModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var items = controllerContext.RequestContext.HttpContext.Request.Form;
            if (controllerContext.RequestContext.HttpContext.Request.UserHostAddress != "195.149.229.109")
            {
                bindingContext.ModelState.AddModelError("InvalidClientAddress", "Response does not come from valid IP address");
                return null;
            }

            return TransferujPlResponse.FromNameValueCollection(items);
        }
    }
}
