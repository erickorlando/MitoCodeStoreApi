using System.Collections.Generic;

namespace MitoCodeStore.Dto.Response
{
    public class CollectionBaseDtoResponse<TDtoClass>
    where TDtoClass : class
    {
        public ICollection<TDtoClass> Collection { get; set; }
        public int TotalPages { get; set; }
    }
}