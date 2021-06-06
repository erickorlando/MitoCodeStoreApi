using System;
using Microsoft.AspNetCore.Identity;

namespace MitoCodeStore.DataAccess
{
    public class MitoCodeUserIdentity : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}