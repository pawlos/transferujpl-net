using System.Web.Mvc;

namespace Transferujpl.Web.Helpers.ActionResult
{
    public class TransferujPlActionResult : System.Web.Mvc.ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            context.RequestContext.HttpContext.Response.Write("YES");
        }
    }
}
