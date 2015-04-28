using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Added libraries
using Microsoft.IdentityModel.Web;
using System.Web;


// This class is only necessary because WsFed SSO does not work out of the box
// with MVC applications, as it does with asp.net.
namespace Red
{
    class WsFedSSOutAuthenticationModule : WSFederationAuthenticationModule
    {
        protected override void InitializeModule(System.Web.HttpApplication context)
        {
            base.InitializeModule(context);
            context.PostAcquireRequestState += context_PostAcquireRequestState;
        }

        public override bool CanReadSignInResponse(System.Web.HttpRequest request, bool onPage)
        {
            var wa = request.Params["wa"];
            return !"wsignoutcleanup1.0".Equals(wa) && base.CanReadSignInResponse(request, onPage);
        }

        private void context_PostAcquireRequestState(object sender, EventArgs e)
        {
            var wa = HttpContext.Current.Request.Params["wa"];
            if (!"wsignoutcleanup1.0".Equals(wa)) return;

            base.CanReadSignInResponse(HttpContext.Current.Request, false);
        }
    }
}
