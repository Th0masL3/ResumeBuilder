﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilder.Models
{
    public class WorkExperience
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }

        public int YearsWorked { get; set; }
    }
}
