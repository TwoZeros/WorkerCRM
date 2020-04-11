﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkerCRM.Models.Interfaces;

namespace WorkerCRM.Models
{
    public class Position : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsAllowedDelete { get; set; }
    }
}
