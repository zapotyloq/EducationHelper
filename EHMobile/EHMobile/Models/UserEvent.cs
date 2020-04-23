using System;
using System.Collections.Generic;
using System.Text;

namespace EHMobile.Models
{
    public class UserEvent
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public int Total { get; set; }
        public decimal Progress { get; set; }
    }
}
