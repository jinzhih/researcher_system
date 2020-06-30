using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAP.Research
{
    enum OutputType
    {
        Conference,
        Journal,
        Other
    }

    class Publication
    {
        public string DOI { set; get; }
        public string Title { set; get; }
        public string Authors { set; get; }
        public int Year { set; get; }
        public OutputType OutputType { set; get; }
        public string CiteAs { set; get; }
        public DateTime AvaDate { set; get; }

        public int Age { set; get; }
        
       // public int PubYear { set; get; }
        

        public override string ToString()
        {
            return Year + "    " + Title;
            
        }
    }
}
