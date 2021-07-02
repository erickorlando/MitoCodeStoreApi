using System;
using System.ComponentModel.DataAnnotations;

namespace MitoCodeStore.Dto.Request
{
    public class CustomerDtoRequest 
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El Nombre no puede exceder de 50")]
        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string NumberId { get; set; }
    }
}
