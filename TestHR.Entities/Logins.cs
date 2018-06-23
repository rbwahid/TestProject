using System;
using System.ComponentModel.DataAnnotations;
namespace TestHR.Entities
{
    public class Logins
    {
        [Key]
        public int LoginsId { get; set; }
        public string UserId { get; set; }
        public string SessionId { get; set; }
        public bool LoggedIn { get; set; }
        public DateTime? LoggedInDateTime { get; set; }
    }
}
