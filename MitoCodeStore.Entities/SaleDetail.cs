namespace MitoCodeStore.Entities
{
    public class SaleDetail : EntityBase

    {
        public Sale Sale { get; set; }
        public int SaleId { get; set; }
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }

    }
}