//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NotEduWeb.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class NotEduWebEntities : DbContext
    {
        public NotEduWebEntities()
            : base("name=NotEduWebEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notes>()
                    .Property(x => x.valeur)
                    .HasPrecision(4, 2);

            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cours> Cours { get; set; }
        public virtual DbSet<Eleves> Eleves { get; set; }
        public virtual DbSet<Notes> Notes { get; set; }
        public virtual DbSet<Promotions> Promotions { get; set; }
    }
}
