using System.Collections.Generic;

namespace MitoCodeStore.Dto.Response
{
    public class SaleDetailDtoResponse 
    {
        public ICollection<SaleDetailDtoSingleResponse> Details { get; set; }
    }
}