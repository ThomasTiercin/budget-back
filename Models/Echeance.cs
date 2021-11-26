using System.ComponentModel.DataAnnotations.Schema;
namespace Budget.Models
{
    public class Echeance
    {
        public string Id { get; set; }
        public string Date { get; set; }
        
        public string TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual Type Type { get; set; }
    }
}
