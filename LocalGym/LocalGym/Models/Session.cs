using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocalGym.Models
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }

        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public Member Member { get; set; }

        [ForeignKey("Trainer")]
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }

        public DateTime SessionDate { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
