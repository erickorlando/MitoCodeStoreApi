namespace MitoCodeStore.Dto.Response
{
    public class ProductSingleDtoResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductUrlImage { get; set; }
        public bool ProductEnabled { get; set; }
    }
}