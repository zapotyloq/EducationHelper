using System;
using System.Collections.Generic;
using System.Text;

namespace Mobile.Models
{
    public class Event
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
    }
}
