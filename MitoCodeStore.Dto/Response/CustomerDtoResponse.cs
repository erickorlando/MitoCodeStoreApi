using System.Collections.Generic;

namespace MitoCodeStore.Dto.Response
{
    public class CustomerDtoResponse
    {
        public ICollection<CustomerDtoSingleResponse> Customers { get; set; }
        public int TotalPages { get; set; }
    }

    public class CustomerDtoSingleResponse
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerBirth { get; set; }
        public string CustomerNumberId { get; set; }
    }
}