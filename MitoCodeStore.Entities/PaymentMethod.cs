using System.ComponentModel.DataAnnotations;

namespace MitoCodeStore.Entities
{
    public class PaymentMethod : EntityBase
    {
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
    }
}