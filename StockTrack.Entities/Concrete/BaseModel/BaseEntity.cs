﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTrack.Entities.Concrete.BaseModel
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public bool Active { get; set; }
    }
}
