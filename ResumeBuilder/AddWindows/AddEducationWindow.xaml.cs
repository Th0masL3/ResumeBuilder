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
    public partial class AddEducationWindow : Window
    {
        private DatabaseHandler db = DatabaseHandler.Instance;

        public AddEducationWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new Education object
            Education newEducation = new Education
            {
                Certification = CertificationTextBox.Text,
                SchoolName = SchoolNameTextBox.Text,
                YearGraduated = Convert.ToInt32(YearGraduatedTextBox.Text) // Assuming YearGraduated is an integer
            };

            // Add the new Education to the database
            int result = db.AddEducation(newEducation);

            if (result > 0) // Assuming AddEducation returns the ID (or a positive value indicating success)
            {
                MessageBox.Show("Education information added successfully.");
                this.DialogResult = true; // Set the dialog result to true
                this.Close(); // Close the window
            }
            else
            {
                MessageBox.Show("Error adding education information.");
            }
        }
    }
}
