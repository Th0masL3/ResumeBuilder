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
using System.Windows.Shapes;

namespace ResumeBuilder.EditWindows
{
    public partial class EditEducation : Window
    {
        private DatabaseHandler db = DatabaseHandler.Instance;
        private Education currentEducation;

        public EditEducation(Education education)
        {
            InitializeComponent();
            currentEducation = education;
            LoadData();
        }

        private void LoadData()
        {
            CertificationTextBox.Text = currentEducation.Certification;
            SchoolNameTextBox.Text = currentEducation.SchoolName;
            YearGraduatedTextBox.Text = currentEducation.YearGraduated.ToString();
        }

        private void EditCertification_Click(object sender, RoutedEventArgs e)
        {
            CertificationTextBox.IsReadOnly = false;
            CertificationTextBox.Focus();
        }

        private void EditSchoolName_Click(object sender, RoutedEventArgs e)
        {
            SchoolNameTextBox.IsReadOnly = false;
            SchoolNameTextBox.Focus();
        }

        private void EditYearGraduated_Click(object sender, RoutedEventArgs e)
        {
            YearGraduatedTextBox.IsReadOnly = false;
            YearGraduatedTextBox.Focus();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            currentEducation.Certification = CertificationTextBox.Text;
            currentEducation.SchoolName = SchoolNameTextBox.Text;
            currentEducation.YearGraduated = Convert.ToInt32(YearGraduatedTextBox.Text); // Handle conversion and validation

            bool updateSuccess = db.EditEducation(currentEducation);
            if (updateSuccess)
            {
                MessageBox.Show("Education information updated successfully.");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error updating education information.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            bool deleteSuccess = db.DeleteEducation(currentEducation.Id);
            if (deleteSuccess)
            {
                MessageBox.Show("Education record deleted successfully.");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error deleting education record.");
            }
        }
    }
}
