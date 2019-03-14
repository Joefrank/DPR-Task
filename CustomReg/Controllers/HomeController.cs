using System.Web.Mvc;

namespace CustomReg.Controllers
{
    public class HomeController : Controller
    {
        // This is the main landing page of the application
        public ActionResult Index()
        {
            return View();
        }
    }
}