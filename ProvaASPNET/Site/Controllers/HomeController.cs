using System.Web.Mvc;

namespace Site.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Home()
        {
            return View();
        }
    }
}