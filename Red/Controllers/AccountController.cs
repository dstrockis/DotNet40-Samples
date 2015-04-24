using System.Web.Mvc;
using Red.Models;

namespace Red.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Login()
        {
            // TODO 
            return new ContentResult() { Content = "show us the login process and come back. You are encouraged to demonstrate advanced features of using authentication module or filter to redirect to login page. " };
        }
         
        public ActionResult Logout()
        {
            // TODO 
            return new ContentResult() { Content = "show us the out process and affection to other apps" };
        }

        [HttpPost]
        public ActionResult CreateUser(CreateUserModel user)
        {
            // TODO
            return new ContentResult() {Content = "Show creation result"};
        }
    }
}
