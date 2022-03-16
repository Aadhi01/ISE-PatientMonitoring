using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.DTO
{
    public class PatientVisit
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string ReasonForVisit { get; set; }
        public DateTime VisitDate { get; set; }
        public char Gender { get; set; }
        public string Details { get; set; }
    }
}
