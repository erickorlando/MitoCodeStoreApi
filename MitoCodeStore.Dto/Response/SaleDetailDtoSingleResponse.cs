namespace MitoCodeStore.Dto.Response
{
    public class SaleDetailDtoSingleResponse
    {
        public int ItemId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}