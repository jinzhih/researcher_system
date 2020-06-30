using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAP.Database;

namespace RAP.Research
{
    enum EmploymentLevel
    {
        All,
        Student,
        A,
        B,
        C,
        D,
        E
    }

    enum Type
    {
        Student,
        Staff
    }

    enum Campus
    {
        Hobart,
        Launceston,
        CradleCoast
    }

    enum Order
    {
        OldestFirst,
        NewestFirst
    }

    class Researcher
    {
        public int Id { get; set; }

        public string GivenName { get; set; }

        public string FamilyName { get; set; }

        public string FullName { get; set; }

        public string Title { get; set; }

        public List<Publication> Publications { get; set; }

        public int PubCount { get; set; }

        public string School { get; set; }

        public Campus Campus { get; set; }

        public string Email { get; set; }

        public string Photo { get; set; }

        public EmploymentLevel Level { set; get; }

        public Type Type { set; get; }

        public string Unit { set; get; }

        public DateTime UtasStart { set; get; }

        public DateTime CurrentStart { set; get; }

        public double Tenure { get; set; }

        public string CurrentJob { get; set; }

        public override string ToString()
        {

            return FamilyName + ", " + GivenName + " (" + Title + ")";

        }
    }
}
