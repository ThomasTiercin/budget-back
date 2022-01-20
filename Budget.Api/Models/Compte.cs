using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace Budget.Api.Models
{
    public class Compte
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
