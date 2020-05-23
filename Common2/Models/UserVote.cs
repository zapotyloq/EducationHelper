using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class UserVote
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChoiseId { get; set; }
        public int VoteId { get; set; }
    }
}
