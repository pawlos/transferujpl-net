using System.Web.Mvc;
using Transferujpl.Core;
using Transferujpl.Web.Mvc.ActionResults;
using Transferujpl.Web.Mvc.Binders;

namespace Transferujpl.Web.Mvc.Controllers
{
    public class DemoController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult Failure()
        {
            return View();
        }

        public ActionResult Notification([ModelBinder(typeof(TransferujPlResponseModelBinder))]TransferujPlResponse response)
        {
            if (response != null && ModelState.IsValid)
            {
                
            }
            return new TransferujPlActionResult();
        }
    }
}