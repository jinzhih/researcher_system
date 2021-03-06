﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RAP.Research;
using RAP.Control;

namespace RAP.View
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

          //  Action doSomething;
            ResearcherBasicController mycon = new ResearcherBasicController();

            List<Researcher> listResearcher = mycon.Workers;

            //The use of a delegate here is not necessa ry, but a remnant of the Week 7 tutorial
            //  doSomething = mycon.Display;

            lbResearcher.ItemsSource = listResearcher; 
        }

        private void LbResearcher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //get selected researcher object
            Researcher researcherSel = (Researcher)lbResearcher.SelectedItem;
            ResearcherBasicController fullconn = new ResearcherBasicController();
            List<Researcher> researcherFull = fullconn.Workers;
            List<Researcher> selRes = new List<Researcher>();
            foreach (Researcher i in researcherFull)
            {
                if (i.id == researcherSel.id)
                {
                    selRes.Add(researcherSel);
                    LBIid.Content = i.id;
                    LBIFN.Content = i.GivenName;
                    LBILN.Content = i.FamilyName;
                    LBIemail.Content = i.Email;
                    LBIcampus.Content = i.Campus;
                    LBIschool.Content = i.School;
                    LBItitle.Content = i.Title;
                }
            }
            //lbResearcherDetails.ItemsSource = selRes;

           


        }
    }
}
