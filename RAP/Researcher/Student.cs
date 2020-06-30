using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Research
{
    class Student : Researcher
    {
        public string Degree { set; get; }

        public int SupervisorId { set; get; }

        public string SupervisorName { set; get; }

        public override string ToString()
        {
            return FullName;
        }
    }

}
