using System;
using System.ComponentModel.DataAnnotations;

namespace MitoCodeStore.Entities
{
    public class Sale : EntityBase
    {
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        
        [StringLength(15)]
        public string InvoiceNumber { get; set; }

        public DateTime Date { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}