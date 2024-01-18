using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LTTQ_DoAn.View
{
    /// <summary>
    /// Interaction logic for AddField.xaml
    /// </summary>
    public partial class AddField : Window
    {
        public AddField()
        {
            InitializeComponent();
        }
        private void addFieldImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files|*.bmp;*.jpg;*.png";
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == true)
            {
                Field_Image.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
