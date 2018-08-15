using ChaabiDocHF.CustomAuthentication;
using ChaabiDocHF.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;


namespace ChaabiDocHF.Controllers
{   
    public class AccountController : Controller
    {
        private ChaabiDocHFEntities dbContext = new ChaabiDocHFEntities();

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string ReturnUrl = "")
        {
            if (User.Identity.IsAuthenticated)
            {
                return LogOut();
            }
            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(LoginView loginView, string ReturnUrl = "")
        {
            if (ModelState.IsValid)
            {
                if (Membership.ValidateUser(loginView.matriculeUser, HashPassword(loginView.mdpUser)))
                {
                    var user = (CustomMembershipUser)Membership.GetUser(loginView.matriculeUser, false);
                    if (user != null)
                    {
                        CustomSerializeModel userModel = new Models.CustomSerializeModel()
                        {
                            idUser = user.idUser,
                            matriculeUser = user.matriculeUser,
                            nomUser = user.nomUser,
                            prenomUser = user.prenomUser,
                            nomRoleListe = user.Roles.Select(r => r.nomRole).ToList()
                        };

                        string userData = JsonConvert.SerializeObject(userModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
                            (
                            1, loginView.matriculeUser, DateTime.Now, DateTime.Now.AddMinutes(15), false, userData
                            );

                        string enTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie("Cookie1", enTicket);
                        Response.Cookies.Add(faCookie);
                    }

                    if (Url.IsLocalUrl(ReturnUrl))
                    {
                        return Redirect(ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Editique", "ChaabiDoc");
                         
                    }
                }
            }
            ModelState.AddModelError("", "Matricule ou mot de passe invalides");
            return View(loginView);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Registration()
        {       
            List<Branche> ListeBranche = dbContext.Branches.ToList();
            List<Role> ListeRole = dbContext.Roles.ToList();

            ViewBag.ListeBranche = new SelectList(ListeBranche, "idBranche", "codeBranche");
            ViewBag.Role= new SelectList(dbContext.Roles, "idRole", "nomRole");

            return View();
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Registration(RegistrationView registrationView)
        {
            //List<Branche> ListeBranche = dbContext.Branches.ToList();
            //List<Role> ListeRole = dbContext.Roles.ToList();

            //ViewBag.ListeBranche = new SelectList(ListeBranche, "idBranche", "codeBranche");
            //ViewBag.ListeRole = new SelectList(ListeRole, "idRole", "nomRole");

            bool statusRegistration = false;
            string messageRegistration = string.Empty;

            if (ModelState.IsValid)
            {

                // Matricule Verification 
                try
                {
                    MembershipUser UserNameByMatricule = Membership.GetUser(registrationView.matriculeUser);
                    if (UserNameByMatricule != null)
                    {
                        messageRegistration = "Cette Matricule existe déjà";
                        ViewBag.Message = messageRegistration;
                        return View(registrationView);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                //Save User Data   
                var user = new Utilisateur()
               {
                   matriculeUser = registrationView.matriculeUser,
                   idBranche = registrationView.idBranche,
                   Roles =  dbContext.Roles.Where(x=>x.idRole==registrationView.role).ToList(),
                   nomUser = registrationView.nomUser,
                   prenomUser = registrationView.prenomUser,
                   mdpUser = HashPassword(registrationView.Password),
                   emailUser = registrationView.emailUser,
                   adresseUser = registrationView.adresseUser,
                   sexeUser = registrationView.sexeUser,
                   cinUser = registrationView.cinUser,
                   rememberMeUser = true,
                   isActiveUser = true
               };

               dbContext.Utilisateurs.Add(user);
               dbContext.SaveChanges();
                
                statusRegistration = true;
            }
            else
            {
                List<Branche> ListeBranche = dbContext.Branches.ToList();
                List<Role> ListeRole = dbContext.Roles.ToList();

                ViewBag.ListeBranche = new SelectList(ListeBranche, "idBranche", "codeBranche");
                ViewBag.Role = new SelectList(dbContext.Roles, "idRole", "nomRole");
                messageRegistration = "Veuillez vérifier les données insérées";
            }

            ViewBag.Message = messageRegistration;
            ViewBag.Status = statusRegistration;

            return View(registrationView);
        }
        

        public ActionResult LogOut()
        {
            HttpCookie cookie = new HttpCookie("Cookie1", "");
            cookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie);

            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account", null);
        }
       
        private string HashPassword(string password)
        {
            return CustomEncrypt.GetMD5Hash(password);
        }
    }
}