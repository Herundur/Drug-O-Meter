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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Drug_O_Meter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int counter = 0;
        private void removeButton_Click(object sender, RoutedEventArgs e)
        {
            counter--;
            count.Text = counter.ToString();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            counter++;
            count.Text = counter.ToString();
        }
    }
}
