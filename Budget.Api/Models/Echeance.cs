using System.ComponentModel.DataAnnotations.Schema;
using System;
namespace Budget.Api.Models
{
    public class Echeance
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        
        public Guid TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual Type Type { get; set; }
    }
}
