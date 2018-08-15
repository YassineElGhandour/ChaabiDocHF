using ChaabiDocHF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ChaabiDocHF.CustomAuthentication
{
    public class CustomMembership : MembershipProvider
    {
        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="matriculeUser"></param>  
        /// <param name="mdpUser"></param>  
        /// <returns></returns>  
        public override bool ValidateUser(string matriculeUser, string mdpUser)
        {
            if (string.IsNullOrEmpty(matriculeUser) || string.IsNullOrEmpty(mdpUser))
            {
                return false;
            }

            using (ChaabiDocHFEntities dbContext = new ChaabiDocHFEntities())
            {
                var user = (from us in dbContext.Utilisateurs
                            where string.Compare(matriculeUser, us.matriculeUser, StringComparison.OrdinalIgnoreCase) == 0
                            && string.Compare(mdpUser, us.mdpUser, StringComparison.OrdinalIgnoreCase) == 0
                            && us.isActiveUser == true
                            select us).FirstOrDefault();

                return (user != null) ? true : false;
            }
        }

        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="matriculeUser"></param>  
        /// <param name="mdpUser"></param>  
        /// <param name="emailUser"></param>  
        /// <param name="passwordQuestion"></param>  
        /// <param name="passwordAnswer"></param>  
        /// <param name="isApproved"></param>  
        /// <param name="providerUserKey"></param>  
        /// <param name="status"></param>  
        /// <returns></returns>  
        public override MembershipUser CreateUser(string matriculeUser, string mdpUser, string emailUser, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        /// <summary>  
        ///   
        /// </summary>  
        /// <param name="matriculeUser"></param>  
        /// <param name="userIsOnline"></param>  
        /// <returns></returns>  
        public override MembershipUser GetUser(string matriculeUser, bool userIsOnline)
        {
            using (ChaabiDocHFEntities dbContext = new ChaabiDocHFEntities())
            {
                var user = (from us in dbContext.Utilisateurs
                            where string.Compare(matriculeUser, us.matriculeUser, StringComparison.OrdinalIgnoreCase) == 0
                            select us).FirstOrDefault();

                if (user == null)
                {
                    return null;
                }
                var selectedUser = new CustomMembershipUser(user);

                return selectedUser;
            }
        }

        public override string GetUserNameByEmail(string email)
        {
            using (ChaabiDocHFEntities dbContext = new Models.ChaabiDocHFEntities())
            {
                string matriculeUser = (from u in dbContext.Utilisateurs
                                   where string.Compare(email, u.emailUser) == 0
                                   select u.matriculeUser).FirstOrDefault();

                return !string.IsNullOrEmpty(matriculeUser) ? matriculeUser : string.Empty;
            }
        }

        #region Overrides of Membership Provider  

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

        public override bool EnablePasswordReset
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotImplementedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}