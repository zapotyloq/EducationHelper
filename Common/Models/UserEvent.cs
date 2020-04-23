using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class UserEvent
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int Progress { get; set; }
        public int Total { get; set; }
    }
}
