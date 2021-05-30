namespace MitoCodeStore.Entities.Complex
{
    public class InvoiceDetailInfo
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public decimal Total { get; set; }
    }
}