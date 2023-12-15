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
        Element element;
        public ElementDetails(Element element)
        {
            InitializeComponent();
            this.element = element;

            //display the user
            CategoryNameTextBox.Text= element.CategoryName;
            DescriptionTextBox.Text= element.Description;
            DateTextBox.Text= element.Date;
        }
}
}
