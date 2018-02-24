using System.Threading.Tasks;
using System.Web.Mvc;

namespace Site.Controllers
{
    public class HomeController : Controller
    {

        public async Task<ActionResult> Index()
        {

            return View();
        }
    }
}
