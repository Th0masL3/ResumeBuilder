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
using ResumeBuilder.Models;

namespace ResumeBuilder.AddWindows
{
    public partial class AddReferenceWindow : Window
    {
        private DatabaseHandler db = DatabaseHandler.Instance;

        public AddReferenceWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Reference newReference = new Reference
            {
                Name = NameTextBox.Text,
                Description = DescriptionTextBox.Text,
                ContactInfo = ContactInfoTextBox.Text
            };

            
            int result = db.AddReference(newReference);

            if (result > 0) 
            {
                MessageBox.Show("Reference added successfully.");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error adding reference.");
            }
        }
    }
}
