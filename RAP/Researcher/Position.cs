using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Research
{

    class Position
    {
        public string PositionName { set; get; }
        public DateTime Start { set; get; }
        public DateTime End { set; get; }

        public override string ToString()
        {
            return Start.ToString("yyyy-MM-dd") + "   " + End.ToString("yyyy-MM-dd") + "   " + PositionName;
        }
    }

}
