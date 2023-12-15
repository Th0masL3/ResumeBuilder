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
    public partial class EditContactInfo : Window
    {
        private DatabaseHandler db = DatabaseHandler.Instance;
        private ContactInfo currentContactInfo;

        public EditContactInfo(ContactInfo contactInfo)
        {
            InitializeComponent();
            currentContactInfo = contactInfo;
            LoadData();
        }

        private void LoadData()
        {
            DescriptionTextBox.Text = currentContactInfo.Description;
            TelephoneTextBox.Text = currentContactInfo.Telephone;
            AddressTextBox.Text = currentContactInfo.Address;
        }

        private void EditDescription_Click(object sender, RoutedEventArgs e)
        {
            DescriptionTextBox.IsReadOnly = false;
            DescriptionTextBox.Focus();
        }

        private void EditTelephone_Click(object sender, RoutedEventArgs e)
        {
            TelephoneTextBox.IsReadOnly = false;
            TelephoneTextBox.Focus();
        }

        private void EditAddress_Click(object sender, RoutedEventArgs e)
        {
            AddressTextBox.IsReadOnly = false;
            AddressTextBox.Focus();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            currentContactInfo.Description = DescriptionTextBox.Text;
            currentContactInfo.Telephone = TelephoneTextBox.Text;
            currentContactInfo.Address = AddressTextBox.Text;

            bool updateSuccess = db.EditContactInfo(currentContactInfo);
            if (updateSuccess)
            {
                MessageBox.Show("Contact Info updated successfully.");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error updating contact info.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            bool deleteSuccess = db.DeleteContactInfo(currentContactInfo.Id);
            if (deleteSuccess)
            {
                MessageBox.Show("Contact Info deleted successfully.");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error deleting contact info.");
            }
        }


    }
}
