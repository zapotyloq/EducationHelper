﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int Total { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public int UserTotal { get; set; }
    }
}
