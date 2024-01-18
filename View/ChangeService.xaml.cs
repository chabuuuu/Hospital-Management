using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LTTQ_DoAn.View
{
    /// <summary>
    /// Interaction logic for ChangeField.xaml
    /// </summary>
    public partial class ChangeService : Window
    {
        public ChangeService()
        {
            InitializeComponent();
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
