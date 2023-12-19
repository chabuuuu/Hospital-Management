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

namespace LTTQ_DoAn.View
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MessageBoxCustom : Window
    {
        public MessageBoxCustom(string Title, string Message, MessageType Type, MessageButtons Buttons)
        {
            InitializeComponent();
            txtMessage.Text = Message;
            if (txtMessage.Text.Length > 27)
                txtMessage.Margin = new Thickness(15, 5, 5, 5);
            if (txtMessage.Text.Length > 54)
            {
                txtMessage.Margin = new Thickness(4, 10, 5, 5);
                txtMessage.FontSize = 16;
                txtMessage.Width = 255;
                txtMessage.Height = 55;
                ImgMessage.Margin = new Thickness(0, 0, 0, 5);
            }
            txtTitle.Text = Title;
            switch (Type)
            {
                case MessageType.Success:
                    System.Media.SystemSounds.Beep.Play();
                    ImgMessage.Source = new BitmapImage(new Uri("pack://application:,,,/LTTQ_DoAn;component/Photo/success.png"));
                    break;
                case MessageType.Error:
                    System.Media.SystemSounds.Hand.Play();
                    ImgMessage.Source = new BitmapImage(new Uri("pack://application:,,,/LTTQ_DoAn;component/Photo/fail.png"));
                    break;
            }
            switch (Buttons)
            {
                case MessageButtons.OKCancel:
                    confirm_btn.Visibility = Visibility.Visible;
                    close_btn.Visibility = Visibility.Visible;
                    break;
                case MessageButtons.OK:
                    confirm_btn.Visibility = Visibility.Visible;
                    close_btn.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
    }
    public enum MessageButtons
    {
        OKCancel,
        OK,
    }
    public enum MessageType
    {
        Success,
        Error,
    }
}
