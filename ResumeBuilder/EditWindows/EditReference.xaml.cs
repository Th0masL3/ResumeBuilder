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

namespace ResumeBuilder.EditWindows
{
    public partial class EditReference : Window
    {
        private DatabaseHandler db = DatabaseHandler.Instance;
        private Reference currentReference;

        public EditReference(Reference reference)
        {
            InitializeComponent();
            currentReference = reference;
            LoadData();
        }

        private void LoadData()
        {
            NameTextBox.Text = currentReference.Name;
            DescriptionTextBox.Text = currentReference.Description;
            ContactInfoTextBox.Text = currentReference.ContactInfo;
        }

        private void EditName_Click(object sender, RoutedEventArgs e)
        {
            NameTextBox.IsReadOnly = false;
            NameTextBox.Focus();
        }

        private void EditDescription_Click(object sender, RoutedEventArgs e)
        {
            DescriptionTextBox.IsReadOnly = false;
            DescriptionTextBox.Focus();
        }

        private void EditContactInfo_Click(object sender, RoutedEventArgs e)
        {
            ContactInfoTextBox.IsReadOnly = false;
            ContactInfoTextBox.Focus();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            currentReference.Name = NameTextBox.Text;
            currentReference.Description = DescriptionTextBox.Text;
            currentReference.ContactInfo = ContactInfoTextBox.Text;

            bool updateSuccess = db.EditReference(currentReference);
            if (updateSuccess)
            {
                MessageBox.Show("Reference updated successfully.");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error updating reference.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            bool deleteSuccess = db.DeleteReference(currentReference.Id);
            if (deleteSuccess)
            {
                MessageBox.Show("Reference deleted successfully.");
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error deleting reference.");
            }
        }
    }
}
