using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChaabiDocHF.Models
{
    public class DossierView
    {
        //Id Dossier
        [Required]
        public int idDossier { get; set; }

        //Code Dossier
        [Required(ErrorMessage = "Le code dossier est requis")]
        [Display(Name = "Code Dossier")]
        public string codeDossier { get; set; }

        //Date de création
        [Required(ErrorMessage = "La date est requis")]
        [Display(Name = "Date de création")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? dateCreation { get; set; }

        //Durée Légale
        [Required(ErrorMessage = "La durée légale est requise")]
        [Display(Name = "Durée Légale")]
        public string dureeLegale { get; set; }

        //Durée conventionnelle
        [Required(ErrorMessage = "La durée conventionnelle est requise")]
        [Display(Name = "Durée conventionnelle")]
        public string dureeConv { get; set; }

        //Extra from Produit
        [Required(ErrorMessage = "Le produit est requis")]
        [Display(Name = "Produit")]
        public int idProduit { get; set; }

        //Extra from Boite
        [Required(ErrorMessage = "Le code de la boîte est requise")]
        [Display(Name = "Code Boite")]
        public int idBoite { get; set; }

        //Scary Extra from Armoire
        [Display(Name = "Code Armoire")]
        public int idArmoire { get; set; }

        //User name
        public string FullUserName { get; set; }
          
     }

}