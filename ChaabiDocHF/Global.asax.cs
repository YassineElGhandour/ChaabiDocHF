using ChaabiDocHF.CustomAuthentication;
using ChaabiDocHF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace ChaabiDocHF
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
      
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["Cookie1"];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);

                CustomPrincipal principal = new CustomPrincipal(authTicket.Name);

                principal.idUser = serializeModel.idUser;
                principal.matriculeUser = serializeModel.matriculeUser;
                principal.nomUser = serializeModel.nomUser;
                principal.prenomUser = serializeModel.prenomUser;
                principal.Roles = serializeModel.nomRoleListe.ToArray<string>();
                //principal.Role = serializeModel.nomRole;

                HttpContext.Current.User = principal;
            }
        }
    }
}
