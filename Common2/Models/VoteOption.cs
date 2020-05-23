using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class VoteOption
    {
        public int Id { get; set; }
        public int VoteId { get; set; }
        public string Option { get; set; }
    }
}
