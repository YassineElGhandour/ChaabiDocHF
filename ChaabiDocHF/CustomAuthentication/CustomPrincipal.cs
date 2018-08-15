using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ChaabiDocHF.CustomAuthentication
{
    public class CustomPrincipal : IPrincipal
    {
        #region Identity Properties  

        public int idUser { get; set; }
        public string nomUser { get; set; }
        public string prenomUser { get; set; }
        public string matriculeUser { get; set; }
        public string emailUser { get; set; }
        //public string Role { get; set; }
        public string[] Roles { get; set; }

        #endregion

        public IIdentity Identity
        {
            get; private set;
        }

        public bool IsInRole(string role)
        {
            //Role.Equals(role)
            if (Roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string matriculeUser)
        {
            Identity = new GenericIdentity(matriculeUser);
        }
    }
}