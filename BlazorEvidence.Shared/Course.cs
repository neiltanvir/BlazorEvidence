﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorEvidence.Shared
{
    public class Course
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Duration { get; set; }
        public int StudentId { get; set; }
        //public string? Type { get; set; }
    }
}
