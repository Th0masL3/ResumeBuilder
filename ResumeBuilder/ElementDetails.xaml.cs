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

namespace ResumeBuilder
{
    /// <summary>
    /// Interaction logic for ElementDetails.xaml
    /// </summary>
    public partial class ElementDetails : Window
    {
        Element element2;
        public ElementDetails(Element element)
        {
            InitializeComponent();
            this.element2 = element;

            //display the user
            CategoryNameTextBox.Text = element.CategoryName;
            DescriptionTextBox.Text = element.Description;
            DateTextBox.Text = element.Date;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ElementDBHandler delete = ElementDBHandler.Instance;
            delete.DeleteElement(element2);
            Close();

        }
    }
}
