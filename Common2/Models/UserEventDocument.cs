using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Common.Models
{
    public class UserEventDocument
    {
        public int Id { get; set; }
        public int UserEventId { get; set; }
        //public string FilePath { get; set; }
        public byte[] File { get; set; }
        public short Is_Marked { get; set; }
        public int Amount { get; set; }

    }
}
