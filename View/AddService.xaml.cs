using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;

namespace LTTQ_DoAn.View
{
    /// <summary>
    /// Interaction logic for AddField.xaml
    /// </summary>
    public partial class AddService : Window
    {
        public AddService()
        {
            InitializeComponent();
        }
        private void AddServiceImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files|*.bmp;*.jpg;*.png";
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == true)
            {
                Service_Image.Source = new BitmapImage(new Uri(ofd.FileName));
            }
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true; // Ngăn chặn việc nhập nếu không phải là số
            }
        }
        private bool IsNumeric(string text)
        {
            // Sử dụng Regex để kiểm tra xem chuỗi có chứa ký tự số không
            return Regex.IsMatch(text, "[0-9]");
        }
    }
}
