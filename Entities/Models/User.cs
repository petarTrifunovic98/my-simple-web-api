using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class User
    {
        [Required(ErrorMessage = "ID is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(31, ErrorMessage = "Username can not be longer than 31 characters")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(255, ErrorMessage = "Email can not be longer than 255 characters")]
        public string? Email { get; set; }

        public override string ToString()
        {
            return String.Format("ID: {0}, Username: {1}, Email: {2}", Id, Username, Email);
        }
    }
}