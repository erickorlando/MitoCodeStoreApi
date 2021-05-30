using System.Collections.Generic;

namespace MitoCodeStore.Dto.Response
{
    public class ProductDtoResponse
    {
        public ICollection<ProductDto> Products { get; set; }
        public int TotalPages { get; set; }
    }
}