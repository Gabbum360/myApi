using Microsoft.AspNetCore.Identity;

namespace FirstApi_Project.Authentication.AuthModels
{

    public class User : IdentityUser
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string AccoutType { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public bool IsDeleted { get; set; }
    }
}
