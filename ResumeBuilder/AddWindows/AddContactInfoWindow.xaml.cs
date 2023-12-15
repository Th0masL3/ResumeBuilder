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
using ResumeBuilder.Models;

namespace ResumeBuilder.AddWindows
{
    
    public partial class AddContactInfoWindow : Window
    {
        private DatabaseHandler db = DatabaseHandler.Instance;
        public AddContactInfoWindow()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            ContactInfo newContactInfo = new ContactInfo
            {
                Description = DescriptionTextBox.Text,
                Telephone = TelephoneTextBox.Text,
                Address = AddressTextBox.Text
            };

            int result = db.AddContactInfo(newContactInfo);

            if (result > 0)
            {
                MessageBox.Show("Contact Information added successfully.");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error adding contact information.");
            }
        }
    }
}
