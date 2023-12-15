using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilder.Models
{
    internal class WorkExperience
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Role { get; set; }

        public string YearsWorked { get; set; }
    }
}
