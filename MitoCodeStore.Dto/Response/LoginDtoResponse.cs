using System;
using System.Collections.Generic;

namespace MitoCodeStore.Dto.Response
{
    public class LoginDtoResponse
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
        public DateTime ExpiredDate { get; set; }
        public string UserId { get; set; }
        public int? CustomerId { get; set; }
        public string UserCode { get; set; }
        public string FullName { get; set; }
        public ICollection<ModuleDtoResponse> Modules { get; set; }
    }

    public class ModuleDtoResponse
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}