﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChaabiDocHF.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ChaabiDocHFEntities : DbContext
    {
        public ChaabiDocHFEntities()
            : base("name=ChaabiDocHFEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Armoire> Armoires { get; set; }
        public virtual DbSet<Boite> Boites { get; set; }
        public virtual DbSet<Branche> Branches { get; set; }
        public virtual DbSet<Dossier> Dossiers { get; set; }
        public virtual DbSet<Motif> Motifs { get; set; }
        public virtual DbSet<Mouvement> Mouvements { get; set; }
        public virtual DbSet<Produit> Produits { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; }
    }
}
