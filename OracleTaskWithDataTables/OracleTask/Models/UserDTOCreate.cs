using Microsoft.AspNetCore.Http;
using OracleTask.Entity.Entities;

namespace OracleTask.Models
{
    public class UserDTOCreate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public Location Location { get; set; }
        public IFormFile Image { get; set; }
    }
}
