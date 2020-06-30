using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAP.Research;
using RAP.Database;
using System.Collections.ObjectModel;

namespace RAP.Control
{
    class Controller
    {
        private List<Researcher> researcherBasic;
        public List<Researcher> ResearcherBasic { get { return researcherBasic; } set { } }


        private ObservableCollection<Researcher> viewableResearcher;
        public ObservableCollection<Researcher> VisibleReasearcher { get { return viewableResearcher; } set { } }


       // private ObservableCollection<Publication> viewablePublication;
       // public ObservableCollection<Publication> VisiblePublication { get { return viewablePublication; } set { } }

        public ObservableCollection<Researcher> GetViewableList()
        {
            return VisibleReasearcher;
        }

     //   public ObservableCollection<Publication> GetViewablePubList()
      //  {
       //     return VisiblePublication;
       // }


        public Controller()
        {
            researcherBasic = Database.Database.LoadBasic();
            viewableResearcher = new ObservableCollection<Researcher>(researcherBasic);
        }

        public void LevelFilter(EmploymentLevel employmentLevel)
        {
            var selected = from Researcher r in researcherBasic
                           where employmentLevel == EmploymentLevel.All || r.Level == employmentLevel
                           select r;
            viewableResearcher.Clear();
            //Converts the result of the LINQ expression to a List and then calls viewableResearcher.Add with each element of that list in turn
            selected.ToList().ForEach(viewableResearcher.Add);
        }

        public void NameFilter(string GivenName)
        {
            var selected = from Researcher r in researcherBasic
                           where (GivenName == null || GivenName.Length <= 0) || r.GivenName.ToLower().Contains(GivenName.ToLower()) || r.FamilyName.ToLower().Contains(GivenName.ToLower())

                           select r;
            viewableResearcher.Clear();
            //Converts the result of the LINQ expression to a List and then calls viewableResearcher.Add with each element of that list in turn
            selected.ToList().ForEach(viewableResearcher.Add);
        }

        public List<Publication> YearFilter(List<Publication> pubList, int StartYear, int EndYear)
        {
            List<Publication> listFilted = new List<Publication>();
            var selected = from Publication p in pubList
                           where p.Year >= StartYear && p.Year <= EndYear
                           select p;
            listFilted.Clear();
            //Converts the result of the LINQ expression to a List and then calls viewableResearcher.Add with each element of that list in turn
            selected.ToList().ForEach(listFilted.Add);
            return listFilted;

        }

        public List<Publication> PubOldFirst(List<Publication> pubList)
        {
            List<Publication> newOrder = new List<Publication>();
            var res = from Publication p in pubList
                      orderby p.Year 
                      select p;
            newOrder.Clear();
            res.ToList().ForEach(newOrder.Add);

            return newOrder;
        }

        public List<Publication> PubNewFirst(List<Publication> pubList)
        {
            List<Publication> newOrder = new List<Publication>();
            var res = from Publication p in pubList
                      orderby p.Year descending
                      select p;
            newOrder.Clear();
            res.ToList().ForEach(newOrder.Add);

            return newOrder;
        }


    }

}
