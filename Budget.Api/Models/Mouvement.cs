using System.ComponentModel.DataAnnotations.Schema;
namespace Budget.Api.Models
{
    public class Mouvement
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string Montant { get; set; }
        public string CategorieId { get; set; }
        public string OrganismeId { get; set; }
        public string CompteId { get; set; }
        public string EcheanceId { get; set; }
        [ForeignKey("CategorieId")]
        public virtual Categorie Categorie { get; set; }
        [ForeignKey("OrganismeId")]
        public virtual Organisme Organisme { get; set; }
        [ForeignKey("CompteId")]
        public virtual Compte Compte { get; set; }
        [ForeignKey("EcheanceId")]
        public virtual Echeance Echeance { get; set; }
    }
}
