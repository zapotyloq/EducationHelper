using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class UserEventDocument
    {
        public int Id { get; set; }
        public int UserEventId { get; set; }
        public string FilePath { get; set; }
        public byte[] File { get; set; }
    }
}
