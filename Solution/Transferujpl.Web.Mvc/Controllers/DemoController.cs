using System.Web.Mvc;
using Transferujpl.Web.Helpers.Binders;
using Transferujpl.Web.Helpers.Models;

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
                Response.Write("YES");
            }
            return new EmptyResult();
        }
    }
}