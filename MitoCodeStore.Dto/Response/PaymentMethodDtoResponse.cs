using System.Collections.Generic;

namespace MitoCodeStore.Dto.Response
{
    public class PaymentMethodDtoResponse
    {
        public ICollection<PaymentMethodDtoSingleResponse> Collection { get; set; }
    }
}