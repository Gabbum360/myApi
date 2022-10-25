/*using Microsoft.AspNet.Identity.EntityFramework;*/
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;

namespace Important.AuthModel
{

    public class User : IdentityUser<Guid>
    {
       
        public string Name { get; set; }
        public string AccoutType { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public bool IsDeleted { get; set; }
    }
}
