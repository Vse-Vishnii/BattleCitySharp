﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleCitySharp
{
    public abstract class MonoBehavior
    {
        protected GameObject GameObject { get; set; }
        protected Transform Transform { get; set; }
    }
}