using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LocalGym.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime JoinDate { get; set; }

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }
}
