using System.ComponentModel.DataAnnotations;

namespace Entities.Models
{
    public class User
    {
        [Required(ErrorMessage = "ID is required")]
        public int id { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [StringLength(31, ErrorMessage = "Username can not be longer than 31 characters")]
        public string? username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(255, ErrorMessage = "Email can not be longer than 255 characters")]
        public string? email { get; set; }

        public override string ToString()
        {
            return String.Format("ID: {0}, Username: {1}, Email: {2}", id, username, email);
        }
    }
}