using System.ComponentModel.DataAnnotations;

namespace MitoCodeStore.Dto.Request
{
    public class ProductDtoRequest
    {
        [Required]
        [StringLength(200)]
        public string ProductName { get; set; }
        
        public int CategoryId { get; set; }
        
        public decimal ProductPrice { get; set; }

        public string ProductBase64Image { get; set; }

        public string FileName { get; set; }
        
        public bool ProductEnabled { get; set; }
    }
}