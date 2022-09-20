using System.ComponentModel.DataAnnotations;
namespace FirstApi_Project.Models

{
    public class RegisterModel
    {
/*        public int Id { get; set; }
*/
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        /*[Required]*/
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        /*[Required]*/
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

    }
}
