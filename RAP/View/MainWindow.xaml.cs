using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using RAP.Research;
using RAP.Control;
using RAP.Database;

namespace RAP.View
{
    /// <summary>
    /// MainWindow.xaml 
    /// </summary>
    public partial class MainWindow : Window
    {
        private Controller controller = new Controller();

        Researcher researcherSel = new Researcher();
        List<Publication> listPublication = new List<Publication>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            List<Researcher> listResearcher = controller.ResearcherBasic;

            lbResearcher.ItemsSource = listResearcher; 
        }

        private void LbResearcher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get selected researcher object
            Researcher researcherSel = (Researcher)lbResearcher.SelectedItem;

            if ( researcherSel == null )
            {
                return;
            }
            //Initialize Component
            btnShowSupName.Visibility = Visibility.Collapsed;
            lbSupName.Visibility = Visibility.Collapsed;
            lbPrePosition.Visibility = Visibility.Collapsed;
            lablePrePosition.Visibility = Visibility.Collapsed;
            lbPrePosition.ItemsSource = null;

            string reseacherType = Convert.ToString(researcherSel.Type);

            if( reseacherType == "Staff")
            {
                Staff staff = new Staff();
                staff = Database.Database.LoadStaffDetails(researcherSel.Id);
                staff.PubCount = Database.Database.PubCounts(researcherSel.Id);
                staff.SupervisionsCount = Database.Database.GetSupCount(researcherSel.Id);
                // create a varible TYAve to store 3-year average value
                // we will pass it to GetPerformance function below 
                double TYAve = Database.Database.GetTYAve(researcherSel.Id);
                staff.TYAve = TYAve;
                staff.Performance = Database.Database.GetPerformance(researcherSel.Level, TYAve);
                spResearcherDetails.DataContext = staff;
                // Switch "Show Name" button visibility to visible for staff whose PubCounts are not 0.
                if(staff.SupervisionsCount != 0)
                {
                    btnShowSupName.Visibility = Visibility.Visible;
                }

                lbPrePosition.Visibility = Visibility.Visible;
                lablePrePosition.Visibility = Visibility.Visible;

                lbSupName.ItemsSource = Database.Database.GetSupervisionsList(researcherSel.Id);

                List<Position> listPrePosition = Database.Database.LoadPrePositions(researcherSel.Id);
                lbPrePosition.ItemsSource = listPrePosition;
            }
            else if( reseacherType == "Student")
            {
                Student student = new Student();
                student = Database.Database.LoadStudentDetails(researcherSel.Id);
                student.SupervisorName = Database.Database.GetSupName(researcherSel.Id);
                student.PubCount = Database.Database.PubCounts(researcherSel.Id);
                spResearcherDetails.DataContext = student;
                // Hide the "Show Name" button for student
                btnShowSupName.Visibility = Visibility.Collapsed;
            }

            List<Publication> listPublication = Database.Database.LoadPublications(researcherSel.Id);
            lbPublication.ItemsSource = listPublication;
            researcherSel.Publications = listPublication;


                //clean publication content 
            spPublicationDetails.DataContext = null;
        }       

        private void DBLevel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

          //  spResearcherDetails.DataContext = null;
            ComboBox cb = (ComboBox)sender;
            //Get selected value from ComboBox
            EmploymentLevel level = (EmploymentLevel)Enum.Parse(typeof(EmploymentLevel), cb.SelectedItem.ToString(), false);
            //Call LevelFilter function
            controller.LevelFilter(level);
            lbResearcher.ItemsSource = controller.GetViewableList();

        }

        private void DBLevel_Loaded(object sender, RoutedEventArgs e)
        {
            DBLevel.ItemsSource = System.Enum.GetNames(typeof(EmploymentLevel));
        }

        private void cbSort_Loaded(object sender, RoutedEventArgs e)
        {
            cbSort.ItemsSource = System.Enum.GetNames(typeof(Order));
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            // Get input text from TextBox
            string Input = tbName.Text;
            // Call NameFilter function
            if (Input == "")
            {
                lbResearcher.ItemsSource = null;
            }
            else
            {
                controller.NameFilter(Input);
                lbResearcher.ItemsSource = controller.GetViewableList();
            }
        }

        private void LbPublication_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Publication publicationSel = (Publication)lbPublication.SelectedItem;

            if (publicationSel == null)
            {
                return;
            }
            Publication publicationDetails = new Publication();
            publicationDetails = Database.Database.LoadPublicationDetails(publicationSel.DOI);
            //publicationDetails.PubYear = publicationDetails.Year.Year;

            spPublicationDetails.DataContext = publicationDetails;

        }

        private void BtnShowSupName_Click(object sender, RoutedEventArgs e)
        {
            //When user click Show Name button, display the name list.
            lbSupName.Visibility = Visibility.Visible;
        }

        private void BtnPubSearch_Click(object sender, RoutedEventArgs e)
        {

                List<Publication> listFilted = new List<Publication>();
                Researcher SelResearch = (Researcher)lbResearcher.SelectedItem;
                int StartYear = Convert.ToInt32(cbStartYear.Text);
                int EndYear = Convert.ToInt32(cbEndYear.Text);
                ObservableCollection<Publication> viewablePublication = new ObservableCollection<Publication>(listPublication);
                listFilted = controller.YearFilter(SelResearch.Publications, StartYear, EndYear);
                lbPublication.ItemsSource = listFilted;
 
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            //  spResearcherDetails.DataContext = null;
            ComboBox cbSort = (ComboBox)sender;
            List<Publication> listFilted = new List<Publication>();
            Researcher SelResearch = (Researcher)lbResearcher.SelectedItem;
            //Get selected value from ComboBox
            string orderType = cbSort.SelectedItem.ToString();
            if (orderType == "OldestFirst")
            {
                listFilted = controller.PubOldFirst(SelResearch.Publications);
            }
            else
            {
                listFilted = controller.PubNewFirst(SelResearch.Publications);
            }

            lbPublication.ItemsSource = listFilted;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Researcher researcherSel = (Researcher)lbResearcher.SelectedItem;

            if (researcherSel == null)
            {
                return;
            }

            CumulativeCount w2 = new CumulativeCount();
            w2.id = researcherSel.Id;
            w2.Show();
        }
    }
}
