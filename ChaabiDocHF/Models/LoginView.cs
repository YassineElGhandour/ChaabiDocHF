using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChaabiDocHF.Models;

namespace ChaabiDocHF.Models
{
    public class LoginView
    {
        [Required]
        [Display(Name = "Matricule")]
        public string matriculeUser { get; set; }
        [Required]
        [Display(Name = "Mot de passe")]
        public string mdpUser { get; set; }
        [Display(Name = "Remember Me")]
        public bool rememeberMe { get; set; }
    }

    public class CustomSerializeModel
    {
        public int idUser { get; set; }
        public string nomUser { get; set; }
        public string prenomUser { get; set; }
        public string matriculeUser { get; set; }
        //public string nomRole { get; set; }
        public List<string> nomRoleListe { get; set; }
        //public ICollection<Role> Roles { get; set; }
    }

    public class RegistrationView
    {

        //Matricule User
        [Required(ErrorMessage = "La matricule est requise")]
        [Display(Name = "Matricule")]
        public string matriculeUser { get; set; }

        //Email User
        [Required(ErrorMessage = "L'Email est requis")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string emailUser { get; set; }

        //Nom User
        [Required(ErrorMessage = "Le nom requis")]
        [Display(Name = "Nom")]
        public string nomUser { get; set; }

        //Prenom User
        [Required(ErrorMessage = "Le prénom est requis")]
        [Display(Name = "Prénom")]
        public string prenomUser { get; set; }

        //Adresse User
        [Required(ErrorMessage = "L'adresse est requise")]
        [Display(Name = "Adresse")]
        public string adresseUser { get; set; }

        //Sexe User
        [Required(ErrorMessage = "Le sexe est requis")]
        [Display(Name = "Sexe")]
        public string sexeUser { get; set; }

        //CIN User
        [Required(ErrorMessage = "Le CIN est requis")]
        [Display(Name = "CIN")]
        public string cinUser { get; set; }

        //Password User
        [Required(ErrorMessage = "Veuillez insérer le mot de passe")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"(?=^.{8,}$)((?=.*\d)(?=.*\W+))(?![.\n])(?=.*[A-Z])(?=.*[a-z]).*$", ErrorMessage = "Le mot de passe doit contenir au moins : une lettre Majuscule, une lettre miniscule, un nombre ou un caractére spécial, et 8 caractéres minimum.")]
        public string Password { get; set; }

        //Confirm Password
        [Required(ErrorMessage = "Veuillez insérer la conformation du mot de passe")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Le mot de passe n'est pas bien confirmé")]
        public string ConfirmPassword { get; set; }

        //Extra from Branches
        [Required(ErrorMessage = "Veuillez choisir une branche")]
        [Display(Name = "Branche")]
        public int idBranche { get; set; }

        ////Extra from idRole
        //[Required(ErrorMessage = "Veuillez choisir un rôle")]
        //[Display(Name = "Role")]
        //public int idRole { get; set; }

        //Extra from idRole
        [Required(ErrorMessage = "Veuillez choisir un rôle")]
        [Display(Name = "Role")]
        public int role { get; set; }

        ////Extra from Role
        //[Required(ErrorMessage = "Veuillez choisir un rôle")]
        //[Display(Name = "Role")]
        //public ICollection<Role> Roles { get; set; }

    }

    
    //Extra from sexeUser
    public enum SexeUser
    {
        Homme,
        Femme,
        Autre
    }

}