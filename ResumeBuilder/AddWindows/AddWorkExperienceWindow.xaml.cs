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

namespace ResumeBuilder.AddWindows
{
    public partial class AddWorkExperienceWindow : Window
    {
        private DatabaseHandler db = DatabaseHandler.Instance;

        public AddWorkExperienceWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            
            WorkExperience newWorkExperience = new WorkExperience
            {
                CompanyName = CompanyNameTextBox.Text,
                Role = RoleTextBox.Text,
                YearsWorked = Convert.ToInt32(YearsWorkedTextBox.Text) 
            };

            
            int result = db.AddWorkExperience(newWorkExperience);

            if (result > 0)
            {
                MessageBox.Show("Work Experience added successfully.");
                this.DialogResult = true; 
                this.Close(); 
            }
            else
            {
                MessageBox.Show("Error adding work experience.");
            }
        }
    }
}