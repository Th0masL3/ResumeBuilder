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
    /// Interaction logic for EditElement.xaml
    /// </summary>
    public partial class EditElement : Window
    {
        Element element2;
        public EditElement(Element element)
        {
            this.element2 = element;
            InitializeComponent();
            CategoryNameTextBox.Text = element.CategoryName;
            DescriptionTextBox.Text = element.Description;
            DateTextBox.Text = element.Date;
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            element2.CategoryName = CategoryNameTextBox.Text;
            element2.Description = DescriptionTextBox.Text;
            element2.Date = DateTextBox.Text;
       
            ElementDBHandler db = ElementDBHandler.Instance;
            db.EditElement(element2);
            Close();

        }
    }
}
