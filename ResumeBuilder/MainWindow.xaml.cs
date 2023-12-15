using ResumeBuilder.AddWindows;
using ResumeBuilder.EditWindows;
using ResumeBuilder.Models;
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

        DatabaseHandler db = DatabaseHandler.Instance;
        List<PersonalInfo> personalInfos;
        public MainWindow()
        {
            InitializeComponent();
            LoadWorkExperience();
            LoadEducation();
            LoadReferences();
            LoadContactInfo();
        }



        private void LoadWorkExperience()
        {
            var workExperiences = db.ReadAllWorkExperiences();
            WorkExperienceDataGrid.ItemsSource = workExperiences;
        }

        private void LoadEducation()
        {
            var educations = db.ReadAllEducation();
            EducationDataGrid.ItemsSource = educations;
        }

        private void LoadReferences()
        {
            var references = db.ReadAllReferences();
            ReferenceDataGrid.ItemsSource = references;
        }

        private void LoadContactInfo()
        {
            var contactInfo = db.ReadAllContactInfo();
            ContactInfoDataGrid.ItemsSource = contactInfo;
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
                    if (windowToOpen.ShowDialog() == true)
                    {
                        LoadWorkExperience(); // Refresh the list after adding
                    }
                }
                else if (clickedButton.Name == "EducationAddButton")
                {
                    windowToOpen = new AddEducationWindow();
                    if (windowToOpen.ShowDialog() == true)
                    {
                        LoadEducation(); // Refresh the list after adding
                    }
                }
                else if (clickedButton.Name == "ReferenceAddButton")
                {
                    windowToOpen = new AddReferenceWindow();
                    if (windowToOpen.ShowDialog() == true)
                    {
                        LoadReferences(); // Refresh the list after adding
                    }
                }
                else if (clickedButton.Name == "ContactInfoAddButton")
                {
                    windowToOpen = new AddContactInfoWindow();
                    if (windowToOpen.ShowDialog() == true)
                    {
                        LoadContactInfo(); // Refresh the list after adding
                    }
                }
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                if (clickedButton.Name == "NameEditButton")
                {
                    NameTextBox.IsReadOnly = false;
                    NameTextBox.Focus();
                    NameSaveButton.Visibility = Visibility.Visible;
                }
                else if (clickedButton.Name == "EmailEditButton")
                {
                    EmailTextBox.IsReadOnly = false;
                    EmailTextBox.Focus();
                    EmailSaveButton.Visibility = Visibility.Visible;
                }
                else if (clickedButton.Name == "PhoneEditButton")
                {
                    PhoneTextBox.IsReadOnly = false;
                    PhoneTextBox.Focus();
                    PhoneSaveButton.Visibility = Visibility.Visible;
                }
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {
                PersonalInfo personalInfo = new PersonalInfo();
                bool isUpdated = false;

                if (clickedButton.Name == "NameSaveButton")
                {
                    personalInfo.Name = NameTextBox.Text;
                    NameTextBox.IsReadOnly = true;
                    NameSaveButton.Visibility = Visibility.Collapsed;
                    isUpdated = db.EditPersonalInfo(personalInfo);
                }
                else if (clickedButton.Name == "EmailSaveButton")
                {
                    personalInfo.Email = EmailTextBox.Text;
                    EmailTextBox.IsReadOnly = true;
                    EmailSaveButton.Visibility = Visibility.Collapsed;
                    isUpdated = db.EditPersonalInfo(personalInfo);
                }
                else if (clickedButton.Name == "PhoneSaveButton")
                {
                    personalInfo.Phone = PhoneTextBox.Text;
                    PhoneTextBox.IsReadOnly = true;
                    PhoneSaveButton.Visibility = Visibility.Collapsed;
                    isUpdated = db.EditPersonalInfo(personalInfo);
                }

                if (isUpdated)
                {
                    MessageBox.Show("Information updated successfully.");
                }
            }
        }

        private void EducationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EducationDataGrid.SelectedItem is Education selectedEducation)
            {
                var editWindow = new EditEducation(selectedEducation);
                editWindow.ShowDialog();

                
                LoadEducation();
            }
        }

        private void WorkExperienceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (WorkExperienceDataGrid.SelectedItem is WorkExperience selectedWorkExperience)
            {
                var editWindow = new EditWorkExperience(selectedWorkExperience);
                if (editWindow.ShowDialog() == true)
                {
                    LoadWorkExperience();
                }
            }
        }

        private void ReferenceDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReferenceDataGrid.SelectedItem is Reference selectedReference)
            {
                var editWindow = new EditReference(selectedReference);
                if (editWindow.ShowDialog() == true)
                {
                    LoadReferences();
                }
            }
        }

        private void ContactInfoDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContactInfoDataGrid.SelectedItem is ContactInfo selectedContactInfo)
            {
                var editWindow = new EditContactInfo(selectedContactInfo);
                if (editWindow.ShowDialog() == true)
                {
                    LoadContactInfo();
                }
            }
        }

        private void ExportPDFButton_Click(object sender, RoutedEventArgs e)
        {
            DatabaseHandler dbHandler = DatabaseHandler.Instance;
            ExportToPDF.exportToPDF(dbHandler);
        }
    }
}