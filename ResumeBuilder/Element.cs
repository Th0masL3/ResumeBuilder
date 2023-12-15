using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ResumeBuilder
{
    public class Element
    {

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public string Date { get; set; }


        public override string ToString()
        {
            string formatted = String.Format("{0}\t {1}\t  {2}\t  {3}\t {4}", Id, CategoryName, Description, Date);
            return formatted;
        }

    }
}
