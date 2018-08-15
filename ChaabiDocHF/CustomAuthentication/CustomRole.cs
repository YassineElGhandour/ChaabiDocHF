using ChaabiDocHF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ChaabiDocHF.CustomAuthentication
{
    public class CustomRole : RoleProvider
    {
        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="matriculeUser"></param>  
        /// <param name="nomRole"></param>  
        /// <returns></returns>  
        public override bool IsUserInRole(string matriculeUser, string nomRole)
        {
            var userRoles = GetRolesForUser(matriculeUser);
            return userRoles.Contains(nomRole);
        }

        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="matriculeUser"></param>  
        /// <returns></returns>  
        public override string[] GetRolesForUser(string matriculeUser)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                return null;
            }

            var userRoles = new string[] { };

            using (ChaabiDocHFEntities dbContext = new ChaabiDocHFEntities())
            {
                var selectedUser = (from us in dbContext.Utilisateurs.Include("Roles")
                                    where string.Compare(us.matriculeUser, matriculeUser, StringComparison.OrdinalIgnoreCase) == 0
                                    select us).FirstOrDefault();


                if (selectedUser != null)
                {
                    //userRoles = new[] { selectedUser.Role.nomRole.ToString() };
                    userRoles = new[] { selectedUser.Roles.Select(r => r.nomRole).ToString() };
                }

                return userRoles.ToArray();
            }         
        }
        #region Overrides of Role Provider  

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }


        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}