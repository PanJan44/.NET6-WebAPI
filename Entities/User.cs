using System;

namespace web_api_net5.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public int RoleID { get; set; }
        public virtual Role Role { get; set; }
    }
}
