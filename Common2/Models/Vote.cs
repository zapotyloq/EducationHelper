using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
