using ChaabiDocHF.CustomAuthentication;
using ChaabiDocHF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using System.Data.Entity;

namespace ChaabiDocHF.Controllers
{
    public class ChaabiDocController : Controller
    {
        private ChaabiDocHFEntities dbContext = new ChaabiDocHFEntities();

        /** ----------------------------------- CREATION ----------------------------------- **/

        [CustomAuthorize(Roles = "RespPV, AgentDev, AgentDevLib")]
        [HttpGet]
        public ActionResult Creation()
        {
            //Send DropDowns Bags for Produits and Boites
            List<Produit> ListeProduits = dbContext.Produits.ToList();
            List <Boite> ListeBoites = dbContext.Boites.ToList();

            ViewBag.ListeProduits = new SelectList(ListeProduits, "idProduit", "nomProduit");
            ViewBag.ListeBoites = new SelectList(ListeBoites, "idBoite", "codeBoite");
           
            return View();
        }

        [CustomAuthorize(Roles = "RespPV, AgentDev, AgentDevLib")]
        [HttpPost]
        public ActionResult Creation(DossierView dossierView)
        {
            bool StatusCreation = false;
            string MessageCreation = string.Empty;

            //Send DropDowns Bags for Produits and Boites
            List<Produit> ListeProduits = dbContext.Produits.ToList();
            List <Boite> ListeBoites = dbContext.Boites.ToList();

            ViewBag.ListeProduits = new SelectList(ListeProduits, "idProduit", "nomProduit");
            ViewBag.ListeBoites = new SelectList(ListeBoites, "idBoite", "codeBoite");

            if (ModelState.IsValid)
            {
                //Get Custom Current User ID
                HttpCookie authCookie = Request.Cookies["Cookie1"];
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);
                CustomPrincipal principal = new CustomPrincipal(authTicket.Name);
                principal.idUser = serializeModel.idUser;

                //Ajout d'un Dossier
                var dossier = new Dossier()
                {
                    idUser = principal.idUser,
                    idProduit = dossierView.idProduit,
                    idBoite = dossierView.idBoite,
                    codeDossier = dossierView.codeDossier,
                    dateCreation = dossierView.dateCreation,
                    dureeLegale = dossierView.dureeLegale,
                    dureeConv = dossierView.dureeConv
                };

                dbContext.Dossiers.Add(dossier);
                dbContext.SaveChanges();

                StatusCreation = true;

            }
            else
            {
                MessageCreation = "Veuillez vérifier les données insérées";
            }

            ViewBag.Message = MessageCreation;
            ViewBag.Status = StatusCreation;

            return View(dossierView);
        }


        /** -------------------------------------------- MOUVEMENT -------------------------------------------- **/

        [CustomAuthorize(Roles = "RespPV, AgentDev, AgentDevLib")]
        [HttpGet]
        public ActionResult Mouvement()
        {
            //Send DropDowns Bags for Dossiers, Motifs, Boites
            List<Dossier> ListeDossiers = dbContext.Dossiers.ToList();
            List<Motif> ListeMotifs = dbContext.Motifs.ToList();

            ViewBag.ListeDossiers = new SelectList(ListeDossiers, "idDossier", "codeDossier");
            ViewBag.ListeMotifs = new SelectList(ListeMotifs, "idMotif", "nomMotif");

            return View();
        }

        [CustomAuthorize(Roles = "RespPV, AgentDev, AgentDevLib")]
        [HttpPost]
        public ActionResult Mouvement(MouvementView mvntView)
        {
            //Important vars
            bool StatusMouvement = false;
            string MessageMouvement = string.Empty;

            //Send DropDowns Bags for Dossiers and Motifs
            List<Dossier> ListeDossiers = dbContext.Dossiers.ToList();
            List<Motif> ListeMotifs = dbContext.Motifs.ToList();

            ViewBag.ListeDossiers = new SelectList(ListeDossiers, "idDossier", "codeDossier");
            ViewBag.ListeMotifs = new SelectList(ListeMotifs, "idMotif", "nomMotif");

            if (ModelState.IsValid)
            {
                //Get Custom Current User ID
                 HttpCookie authCookie = Request.Cookies["Cookie1"];
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                var serializeModel = JsonConvert.DeserializeObject<CustomSerializeModel>(authTicket.UserData);
                CustomPrincipal principal = new CustomPrincipal(authTicket.Name);
                principal.idUser = serializeModel.idUser;

                //Effectuer un mouvement
                var mouvement = new Mouvement()
                {
                    idUser = principal.idUser,
                    idDossier = mvntView.idDossier,
                    idMotif = mvntView.idMotif,
                    dateRest = mvntView.dateRest,
                    dateMvnt = mvntView.dateMvnt,
                    numCont = mvntView.numCont,
                    isValid = false
                };

                dbContext.Mouvements.Add(mouvement);
                dbContext.SaveChanges();

                StatusMouvement = true;
            }
            else
            {
                MessageMouvement = "Veuillez vérifier les données insérées";
            }


            ViewBag.Message = MessageMouvement;
            ViewBag.Status = StatusMouvement;

            return View(mvntView);
        }

        /** -------------------------------------------- EDITIQUE/MOUVEMENT -------------------------------------------- **/

        [CustomAuthorize(Roles = "RespPV, AgentDev, AgentDevLib, Admin")]
        [HttpGet]
        public ActionResult Editique()
        {
            return View(dbContext.Mouvements.ToList());
        }

        //ADD OR EDIT DOSSIER
        [CustomAuthorize(Roles = "RespPV, AgentDev, AgentDevLib, Admin")]
        [HttpGet]
        public ActionResult AddOrEditMouvement(int id = 0)
        {
            List<Dossier> ListeDossiers = dbContext.Dossiers.ToList();
            List<Motif> ListeMotifs = dbContext.Motifs.ToList();
            ViewBag.ListeDossiers = new SelectList(ListeDossiers, "idDossier", "codeDossier");
            ViewBag.ListeMotifs = new SelectList(ListeMotifs, "idMotif", "nomMotif");

            dbContext.Configuration.ProxyCreationEnabled = false;
            if (id == 0)
                return View(new Mouvement());
            else
            {
                return View(dbContext.Mouvements.Where(x => x.idMvnt== id).FirstOrDefault());
            }
        }

        [CustomAuthorize(Roles = "RespPV, AgentDev, AgentDevLib")]
        [HttpPost]
        public ActionResult AddOrEditMouvement(Mouvement mvnt)
        {
            List<Dossier> ListeDossiers = dbContext.Dossiers.ToList();
            List<Motif> ListeMotifs = dbContext.Motifs.ToList();
            ViewBag.ListeDossiers = new SelectList(ListeDossiers, "idDossier", "codeDossier");
            ViewBag.ListeMotifs = new SelectList(ListeMotifs, "idMotif", "nomMotif");

            if (mvnt.idMvnt == 0)
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                dbContext.Mouvements.Add(mvnt);
                dbContext.SaveChanges();
                return Json(new { success = true, message = "Ajout : Succés" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                dbContext.Entry(mvnt).State = EntityState.Modified;
                dbContext.SaveChanges();
                return Json(new { success = true, message = "Mise à jour : Succés" }, JsonRequestBehavior.AllowGet);
            }
        }

        //DELETE MOUVEMENT
        [CustomAuthorize(Roles = "RespPV, AgentDev, AgentDevLib")]
        [HttpPost]
        public ActionResult DeleteMouvement(int id)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            Mouvement mvnt = dbContext.Mouvements.Where(x => x.idMvnt == id).FirstOrDefault();
            dbContext.Mouvements.Remove(mvnt);
            dbContext.SaveChanges();
            return Json(new { success = true, message = "Le mouvement est supprimé" }, JsonRequestBehavior.AllowGet);
        }


        /** ---------------------------------- JsonResults for Ajax Purposes ----------------------------------**/

        public ActionResult GetDossierData()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            var list = new List<DossierView>();
            foreach (var item in dbContext.Dossiers.ToList())
            {
                list.Add(new DossierView {
                    codeDossier = item.codeDossier,
                    dateCreation = item.dateCreation,
                    dureeConv = item.dureeConv,
                    dureeLegale = item.dureeLegale,
                    idBoite= item.idBoite,
                    idDossier = item.idDossier,
                    idProduit = item.idProduit,
                    FullUserName = dbContext.Utilisateurs.Find(item.idUser).FullUserName
                });
            }
            //List<Dossier> dossList = ;
            return Json(new { data = list }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBoiteListe(int idDossier)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            string query = "SELECT * FROM Boite WHERE idBoite = (SELECT idBoite from Dossier Where idDossier = @p0)";
            var ListeBoites = dbContext.Boites.SqlQuery(query, idDossier).ToList();
            return Json(ListeBoites, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetArmoireListe(int idDossier)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            string query = "SELECT * FROM Armoire WHERE idArmoire = (SELECT idArmoire from Boite Where idBoite = (SELECT idBoite FROM Dossier WHERE idDossier = @p0))";
            var ListeArmoires = dbContext.Armoires.SqlQuery(query, idDossier).ToList();
            return Json(ListeArmoires, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetArmoireListeWithIdBoite(int idBoite)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            string query = "SELECT * FROM Armoire WHERE idArmoire = (SELECT idArmoire from Boite Where idBoite = @p0)";
            var ListeArmoires = dbContext.Armoires.SqlQuery(query, idBoite).ToList();
            return Json(ListeArmoires, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDossierListe(int idDossier)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            var ListeDossiers = dbContext.Dossiers.Where(x => x.idDossier == idDossier).ToList();
            return Json(ListeDossiers, JsonRequestBehavior.AllowGet);
        }
        
    }
}