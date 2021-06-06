using System.ComponentModel.DataAnnotations;

namespace MitoCodeStore.Dto.Request
{
    public class PaymentMethodDtoRequest
    {
        [Required]
        public string Name { get; set; }
    }
}