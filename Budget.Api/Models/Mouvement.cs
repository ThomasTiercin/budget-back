using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace Budget.Api.Models
{
    public class Mouvement
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public decimal Montant { get; set; }
        public Guid CategorieId { get; set; }
        public Guid OrganismeId { get; set; }
        public Guid CompteId { get; set; }
        public Guid EcheanceId { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("CategorieId")]
        public virtual Categorie Categorie { get; set; }
        [ForeignKey("OrganismeId")]
        public virtual Organisme Organisme { get; set; }
        [ForeignKey("CompteId")]
        public virtual Compte Compte { get; set; }
        [ForeignKey("EcheanceId")]
        public virtual Echeance Echeance { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
