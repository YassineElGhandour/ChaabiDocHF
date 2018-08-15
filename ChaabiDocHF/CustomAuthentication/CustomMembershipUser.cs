using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ChaabiDocHF.Models;
using System.Web.Security;
using Microsoft.AspNet.Identity;

namespace ChaabiDocHF.CustomAuthentication
{
    public class CustomMembershipUser : MembershipUser
    {
        #region Utilisateur Properties  

        public int idUser { get; set; }
        public string matriculeUser { get; set; }
        public string nomUser { get; set; }
        public string prenomUser { get; set; }
        public string nomRole { get; set; }
        public ICollection<Role> Roles { get; set; }

        #endregion

        public CustomMembershipUser(Utilisateur user):base("CustomMembership", user.matriculeUser, user.idUser, user.emailUser, string.Empty, string.Empty, true, false, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now)
        {
            idUser = user.idUser;
            matriculeUser = user.matriculeUser;
            nomUser = user.nomUser;
            prenomUser = user.prenomUser;
            Roles = user.Roles;    
        }
    }
}