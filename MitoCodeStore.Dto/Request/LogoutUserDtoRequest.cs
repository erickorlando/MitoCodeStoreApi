using System.ComponentModel.DataAnnotations;

namespace MitoCodeStore.Dto.Request
{
    public class LogoutUserDtoRequest
    {
        [Required]
        public string UserName { get; set; }
    }
}