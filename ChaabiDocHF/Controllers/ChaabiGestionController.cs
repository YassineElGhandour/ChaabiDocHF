using ChaabiDocHF.CustomAuthentication;
using ChaabiDocHF.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChaabiDocHF.Controllers
{
    public class ChaabiGestionController : Controller
    {
        private ChaabiDocHFEntities dbContext = new ChaabiDocHFEntities();

        /** -------------------------------------------- Les Dossiers --------------------------------------------**/

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult LesDossiers()
        {
            return View(dbContext.Dossiers.ToList());
        }

        //ADD OR EDIT DOSSIER
        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddOrEditDossier(int id = 0)
        {
            List<Produit> ListeProduits = dbContext.Produits.ToList();
            List<Boite> ListeBoites = dbContext.Boites.ToList();

            ViewBag.ListeProduits = new SelectList(ListeProduits, "idProduit", "nomProduit");
            ViewBag.ListeBoites = new SelectList(ListeBoites, "idBoite", "codeBoite");

            dbContext.Configuration.ProxyCreationEnabled = false;
            if (id == 0)
                return View(new Dossier());
            else
            {
                return View(dbContext.Dossiers.Where(x => x.idDossier == id).FirstOrDefault());
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddOrEditDossier(Dossier dossier)
        {
            List<Produit> ListeProduits = dbContext.Produits.ToList();
            List<Boite> ListeBoites = dbContext.Boites.ToList();

            ViewBag.ListeProduits = new SelectList(ListeProduits, "idProduit", "nomProduit");
            ViewBag.ListeBoites = new SelectList(ListeBoites, "idBoite", "codeBoite");

            if (dossier.idDossier == 0)
            {
                dbContext.Configuration.ProxyCreationEnabled = false;

                string query = "SELECT * FROM Dossier WHERE codeDossier = @p0";
                var dossiers = dbContext.Dossiers.SqlQuery(query, dossier.codeDossier).ToList();
                int count = dossiers.Count();

                if (count == 0)
                {
                    dbContext.Dossiers.Add(dossier);
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Ajout : Succés" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { success = false, message = "Ce dossier existe déjà" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                dbContext.Entry(dossier).State = EntityState.Modified;
                dbContext.SaveChanges();
                return Json(new { success = true, message = "Mise à jour : Succés" }, JsonRequestBehavior.AllowGet);
            }
        }

        //DELETE DOSSIER
        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteDossier(int id)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            Dossier dossier = dbContext.Dossiers.Where(x => x.idDossier == id).FirstOrDefault();
            dbContext.Dossiers.Remove(dossier);
            dbContext.SaveChanges();
            return Json(new { success = true, message = "Le dossier est supprimé" }, JsonRequestBehavior.AllowGet);
        }

        /** -------------------------------------------- Les Branches --------------------------------------------**/

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult LesBranches()
        {
            return View();
        }

        //ADD OR EDIT BRANCHE
        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddOrEditBranche(int id = 0)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            if (id == 0)
                return View(new Branche());
            else
            {
                return View(dbContext.Branches.Where(x => x.idBranche == id).FirstOrDefault());
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddOrEditBranche(Branche branche)
        {
            string query = "SELECT * FROM Branche WHERE codeBranche = @p0";
            var branches = dbContext.Branches.SqlQuery(query, branche.codeBranche).ToList();
            int count = branches.Count();

            if (count == 0)
            {
                if (branche.idBranche == 0)
                {
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    dbContext.Branches.Add(branche);
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Ajout : Succés" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    dbContext.Entry(branche).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Mise à jour : Succés" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Cette branche existe déjà" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [CustomAuthorize(Roles = "Admin")]
        //DELETE BRANCHE
        [HttpPost]
        public ActionResult DeleteBranche(int id)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            Branche branche = dbContext.Branches.Where(x => x.idBranche == id).FirstOrDefault<Branche>();
            dbContext.Branches.Remove(branche);
            dbContext.SaveChanges();
            return Json(new { success = true, message = "La branche est supprimée" }, JsonRequestBehavior.AllowGet);
        }

        /** -------------------------------------------- Les Roles --------------------------------------------**/

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult LesRoles()
        {
            return View();
        }

        //ADD OR EDIT ROLE
        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddOrEditRole(int id = 0)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            if (id == 0)
                return View(new Role());
            else
            {
                return View(dbContext.Roles.Where(x => x.idRole == id).FirstOrDefault());
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddOrEditRole(Role role)
        {
            string query = "SELECT * FROM Role WHERE nomRole = @p0";
            var roles = dbContext.Roles.SqlQuery(query, role.nomRole).ToList();
            int count = roles.Count();

            if (count == 0)
            {
                if (role.idRole == 0)
                {
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    dbContext.Roles.Add(role);
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Ajout : Succés" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    dbContext.Entry(role).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Mise à jour : Succés" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Ce rôle existe déjà" }, JsonRequestBehavior.AllowGet);
            }
        }

        //DELETE ROLE
        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteRole(int id)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            Role role = dbContext.Roles.Where(x => x.idRole == id).FirstOrDefault<Role>();
            dbContext.Roles.Remove(role);
            dbContext.SaveChanges();
            return Json(new { success = true, message = "Le role est supprimé" }, JsonRequestBehavior.AllowGet);
        }

        /** -------------------------------------------- Les Boîtes --------------------------------------------**/

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult LesBoites()
        {
            List<Armoire> ListeArmoires = dbContext.Armoires.ToList();
            ViewBag.ListeArmoires = new SelectList(ListeArmoires, "idArmoire", "codeArmoire");

            return View();
        }

        //ADD OR EDIT BOITE
        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddOrEditBoite(int id = 0)
        {
            List<Armoire> ListeArmoires = dbContext.Armoires.ToList();
            ViewBag.ListeArmoires = new SelectList(ListeArmoires, "idArmoire", "codeArmoire");

            dbContext.Configuration.ProxyCreationEnabled = false;
            if (id == 0)
                return View(new Boite());
            else
            {
                return View(dbContext.Boites.Where(x => x.idBoite == id).FirstOrDefault());
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddOrEditBoite(Boite boite)
        {
            List<Armoire> ListeArmoires = dbContext.Armoires.ToList();
            ViewBag.ListeArmoires = new SelectList(ListeArmoires, "idArmoire", "codeArmoire");

            string query = "SELECT * FROM Boite WHERE codeBoite = @p0";
            var boites = dbContext.Boites.SqlQuery(query, boite.codeBoite).ToList();
            int count = boites.Count();

            if (count == 0)
            {
                if (boite.idBoite == 0)
                {
                    dbContext.Configuration.ProxyCreationEnabled = false;

                    dbContext.Boites.Add(boite);
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Ajout : Succés" }, JsonRequestBehavior.AllowGet);


                }
                else
                {
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    dbContext.Entry(boite).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Mise à jour : Succés" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Cette Boite existe déjà" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        //DELETE BOITE
        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteBoite(int id)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            Boite boite = dbContext.Boites.Where(x => x.idBoite == id).FirstOrDefault<Boite>();
            dbContext.Boites.Remove(boite);
            dbContext.SaveChanges();
            return Json(new { success = true, message = "La boîte est supprimée" }, JsonRequestBehavior.AllowGet);
        }

        /** -------------------------------------------- Les Armoires --------------------------------------------**/

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult LesArmoires()
        {
            return View();
        }

        //ADD OR EDIT ARMOIRE
        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddOrEditArmoire(int id = 0)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            if (id == 0)
                return View(new Armoire());
            else
            {
                return View(dbContext.Armoires.Where(x => x.idArmoire == id).FirstOrDefault());
            }
        }

        [HttpPost]
        [CustomAuthorize(Roles = "Admin")]
        public ActionResult AddOrEditArmoire(Armoire armoire)
        {
            string query = "SELECT * FROM Armoire WHERE codeArmoire = @p0";
            var armoires = dbContext.Armoires.SqlQuery(query, armoire.codeArmoire).ToList();
            int count = armoires.Count();

            if (count == 0)
            {
                if (armoire.idArmoire == 0)
                {
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    dbContext.Armoires.Add(armoire);
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Ajout : Succés" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    dbContext.Entry(armoire).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Mise à jour : Succés" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Cet armoire existe déjà" }, JsonRequestBehavior.AllowGet);
            }
        }

        //DELETE ARMOIRE
        [HttpPost]
        public ActionResult DeleteArmoire(int id)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            Armoire armoire = dbContext.Armoires.Where(x => x.idArmoire == id).FirstOrDefault<Armoire>();
            dbContext.Armoires.Remove(armoire);
            dbContext.SaveChanges();
            return Json(new { success = true, message = "L'armoire est supprimée" }, JsonRequestBehavior.AllowGet);
        }

        /** -------------------------------------------- Les Motifs --------------------------------------------**/

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult LesMotifs()
        {
            return View();
        }

        //ADD OR EDIT MOTIF
        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddOrEditMotif(int id = 0)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            if (id == 0)
                return View(new Motif());
            else
            {
                return View(dbContext.Motifs.Where(x => x.idMotif == id).FirstOrDefault());
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddOrEditMotif(Motif motif)
        {
            string query = "SELECT * FROM Motif WHERE nomMotif = @p0";
            var motifs = dbContext.Motifs.SqlQuery(query, motif.nomMotif).ToList();
            int count = motifs.Count();

            if (count == 0)
            {
                if (motif.idMotif == 0)
                {
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    dbContext.Motifs.Add(motif);
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Ajout : Succés" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    dbContext.Entry(motif).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Mise à jour : Succés" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Ce motif existe déjà" }, JsonRequestBehavior.AllowGet);
            }
        }

        //DELETE MOTIF
        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteMotif(int id)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            Motif motif = dbContext.Motifs.Where(x => x.idMotif == id).FirstOrDefault();
            dbContext.Motifs.Remove(motif);
            dbContext.SaveChanges();
            return Json(new { success = true, message = "Le motif est supprimé" }, JsonRequestBehavior.AllowGet);
        }

        /** -------------------------------------------- Les Produits --------------------------------------------**/

        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult LesProduits()
        {
            return View();
        }

        //ADD OR EDIT MOTIF
        [CustomAuthorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult AddOrEditProduit(int id = 0)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            if (id == 0)
                return View(new Produit());
            else
            {
                return View(dbContext.Produits.Where(x => x.idProduit == id).FirstOrDefault());
            }
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddOrEditProduit(Produit produit)
        {
            string query = "SELECT * FROM Produit WHERE nomProduit = @p0";
            var produits = dbContext.Produits.SqlQuery(query, produit.nomProduit).ToList();
            int count = produits.Count();

            if (count == 0)
            {
                if (produit.idProduit == 0)
                {
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    dbContext.Produits.Add(produit);
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Ajout : Succés" }, JsonRequestBehavior.AllowGet);
                }

                else
                {
                    dbContext.Configuration.ProxyCreationEnabled = false;
                    dbContext.Entry(produit).State = EntityState.Modified;
                    dbContext.SaveChanges();
                    return Json(new { success = true, message = "Mise à jour : Succés" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false, message = "Ce produit existe déjà" }, JsonRequestBehavior.AllowGet);
            }

            
        }

        //DELETE MOTIF
        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteProduit(int id)
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            Produit produit = dbContext.Produits.Where(x => x.idProduit == id).FirstOrDefault();
            dbContext.Produits.Remove(produit);
            dbContext.SaveChanges();
            return Json(new { success = true, message = "Le produit est supprimée" }, JsonRequestBehavior.AllowGet);
        }

        /** ---------------------------------- JsonResults for Ajax Purposes ----------------------------------**/

        public JsonResult GetDossierListe()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            List<Dossier> DossierListe = dbContext.Dossiers.ToList();
            return Json(new { data = DossierListe }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBrancheListe()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            List<Branche> BrancheListe = dbContext.Branches.ToList();
            return Json(new { data = BrancheListe }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetRoleListe()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            List<Role> RoleListe = dbContext.Roles.ToList();
            return Json(new { data = RoleListe }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetBoiteListe()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            List<Boite> BoiteListe = dbContext.Boites.ToList();
            return Json(new { data = BoiteListe }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetArmoireListe()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            List<Armoire> ArmoireListe = dbContext.Armoires.ToList();
            return Json(new { data = ArmoireListe }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMotifListe()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            List<Motif> MotifListe = dbContext.Motifs.ToList();
            return Json(new { data = MotifListe }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProduitListe()
        {
            dbContext.Configuration.ProxyCreationEnabled = false;
            List<Produit> ProduitListe = dbContext.Produits.ToList();
            return Json(new { data = ProduitListe }, JsonRequestBehavior.AllowGet);
        }
    }
}