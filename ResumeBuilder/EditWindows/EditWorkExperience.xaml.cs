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
    public partial class EditWorkExperience : Window
    {
        private DatabaseHandler db = DatabaseHandler.Instance;
        private WorkExperience currentWorkExperience;

        public EditWorkExperience(WorkExperience workExperience)
        {
            InitializeComponent();
            currentWorkExperience = workExperience;
            LoadData();
        }

        private void LoadData()
        {
            CompanyNameTextBox.Text = currentWorkExperience.CompanyName;
            RoleTextBox.Text = currentWorkExperience.Role;
            YearsWorkedTextBox.Text = currentWorkExperience.YearsWorked.ToString();
        }

        private void EditCompanyName_Click(object sender, RoutedEventArgs e)
        {
            CompanyNameTextBox.IsReadOnly = false;
            CompanyNameTextBox.Focus();
        }

        private void EditRole_Click(object sender, RoutedEventArgs e)
        {
            RoleTextBox.IsReadOnly = false;
            RoleTextBox.Focus();
        }

        private void EditYearsWorked_Click(object sender, RoutedEventArgs e)
        {
            YearsWorkedTextBox.IsReadOnly = false;
            YearsWorkedTextBox.Focus();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            currentWorkExperience.CompanyName = CompanyNameTextBox.Text;
            currentWorkExperience.Role = RoleTextBox.Text;
            currentWorkExperience.YearsWorked = int.Parse(YearsWorkedTextBox.Text); // Handle conversion and validation

            bool updateSuccess = db.EditWorkExperience(currentWorkExperience);
            if (updateSuccess)
            {
                MessageBox.Show("Work Experience updated successfully.");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error updating work experience.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            bool deleteSuccess = db.DeleteWorkExperience(currentWorkExperience.Id);
            if (deleteSuccess)
            {
                MessageBox.Show("Work Experience deleted successfully.");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error deleting work experience.");
            }
        }
    }
}
