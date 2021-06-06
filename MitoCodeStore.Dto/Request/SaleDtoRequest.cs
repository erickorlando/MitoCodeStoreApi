using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MitoCodeStore.Entities;

namespace MitoCodeStore.Dto.Request
{
    public class SaleDtoRequest
    {
        public int CustomerId { get; set; }

        [Required]
        [RegularExpression(Constants.DatePattern)]
        public string Date { get; set; }
        
        public int PaymentMethodId { get; set; }

        public ICollection<SaleDetailtDtoRequest> Products { get; set; }

    }

    public class SaleDetailtDtoRequest
    {
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}