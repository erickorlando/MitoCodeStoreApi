using System.ComponentModel.DataAnnotations;

namespace MitoCodeStore.Dto.Request
{
    public class LoginDtoRequest
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        public string Password { get; set; }
    }
}