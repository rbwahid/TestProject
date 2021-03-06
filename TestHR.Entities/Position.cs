﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestHR.Data;

namespace TestHR.Entities
{
    public class Position : Entity
    {
        public virtual Company Company { get; set; }
        public virtual Department Department { get; set; }
        public string Name { get; set; }
        public Position ReportingPosition { get; set; }
    }
}
