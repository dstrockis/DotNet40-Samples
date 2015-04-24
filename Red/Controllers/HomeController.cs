using System.Collections.Generic;
using System.Web.Mvc;
using Common;

namespace Red.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // TODO fill SAML content if the user is authenticated
            ViewBag.SamlContent = "{SAML CONTENT}";
            return View();
        }

        public ActionResult GetUserGroups()
        {
            // TODO call your API to get additional authorization data, user group for example. This should be working for Admin role only

            var usergroups = new List<UserGroup>
            {
                new UserGroup{Id=1, Name = "Dummy group/role 1"},
                new UserGroup{Id=2, Name = "Dummy group/role 2"},
                new UserGroup{Id=3, Name = "Dummy group/role 3"},
            };
            return View(usergroups);
        }

    }
}
