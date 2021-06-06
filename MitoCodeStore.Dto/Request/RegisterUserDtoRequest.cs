using System.ComponentModel.DataAnnotations;
using MitoCodeStore.Entities;

namespace MitoCodeStore.Dto.Request
{
    public class RegisterUserDtoRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(Constants.DatePattern)]
        public string BirthDate { get; set; }

        [Required]
        [Compare(nameof(ConfirmPassword))]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserCode { get; set; }
    }
}