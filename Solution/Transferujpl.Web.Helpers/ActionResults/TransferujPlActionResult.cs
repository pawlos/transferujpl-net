using System.Web.Mvc;

namespace Transferujpl.Web.Mvc.ActionResults
{
    public class TransferujPlActionResult : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            context.RequestContext.HttpContext.Response.Write("YES");
        }
    }
}
