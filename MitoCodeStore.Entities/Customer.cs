using System;
using System.ComponentModel.DataAnnotations;

namespace MitoCodeStore.Entities
{
    public class Customer : EntityBase
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public DateTime BirthDate { get; set; }

        [Required]
        [StringLength(11)]
        public string NumberId { get; set; }

        [StringLength(100)]
        public string Email { get; set; }
    }
}
