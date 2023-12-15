using ResumeBuilder.AddWindows;
using System;
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

namespace ResumeBuilder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ElementDBHandler db = ElementDBHandler.Instance;
        List<Element> element;
        public MainWindow()
        {
            InitializeComponent();
            RefreshAllElementList();
        }

        public void RefreshAllElementList()
        {
            AllElementDataGrid.ItemsSource = null;
            element = db.ReadAllElements();
            AllElementDataGrid.ItemsSource = element;
        }

        private void AllElementDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Element element = (Element)AllElementDataGrid.SelectedItem;
            if (element != null)
            {
                ElementDetailsWindow elementDetailsWindow = new ElementDetailsWindow(element);
                elementDetailsWindow.ShowDialog();
                RefreshElmentList();
            }

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                Window windowToOpen = null;

                if (clickedButton.Name == "WorkExperienceAddButton")
                {
                    windowToOpen = new AddWorkExperienceWindow();
                }
                else if (clickedButton.Name == "EducationAddButton")
                {
                    windowToOpen = new AddEducationWindow();
                }
                else if (clickedButton.Name == "ReferenceAddButton")
                {
                    windowToOpen = new AddReferenceWindow();
                }
                else if (clickedButton.Name == "ContactInfoAddButton")
                {
                    windowToOpen = new AddContactInfoWindow();
                }

                windowToOpen?.ShowDialog(); // Show the window as a modal dialog
            }
        }
    }
}
