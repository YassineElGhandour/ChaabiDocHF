using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChaabiDocHF.Models
{
    public class MouvementView
    {
        //IdMouvement
        [Required]
        public int idMvnt { get; set; }

        //Date Mouvement
        [Required(ErrorMessage = "La date de restitution est requise")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dateRest { get; set; }

        [Required(ErrorMessage = "Le numéro du conteneur est requis")]
        [Display(Name = "Numéro du Conteneur")]
        public String numCont { get; set; }

        //Extra from Dossier
        [Required(ErrorMessage = "La date de mouvement est requise")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime dateMvnt { get; set; }

        //Extra from Dossier
        [Required(ErrorMessage = "Veuillez s'il vous plaît choisir le code dossier")]
        [Display(Name = "Code Dossier")]
        public int idDossier { get; set; }

        //Extra from Motif
        [Required(ErrorMessage = "Veuillez s'il vous plaît choisir le motif de mouvement")]
        [Display(Name = "Motif de mouvement")]
        public int idMotif { get; set; }

        //Scary Extra from Boite
        [Display(Name = "Code Boite")]
        public int idBoite { get; set; }

        //Scary Extra from Armoire
        [Display(Name = "Code Armoire")]
        public int idArmoire { get; set; }

        //Scary Extra from idDateCreation
        [Display(Name = "Date de Création")]
        public int idDateCreation { get; set; }

        //Scary Extra from idDureeConv
        [Display(Name = "Durée Conv")]
        public int idDureeConv { get; set; }

    }
}