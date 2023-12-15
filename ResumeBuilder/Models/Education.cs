using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResumeBuilder.Models
{
    public class Education
    {
        public int Id { get; set; }
        public string Certification { get; set; }
        public string SchoolName { get; set; }
        public int YearGraduated { get; set; }

    }
}
