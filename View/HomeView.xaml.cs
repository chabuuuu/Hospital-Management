using System;
using System.Collections.Generic;
using System.IO;
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

namespace LTTQ_DoAn.View
{
    /// <summary>
    /// Interaction logic for HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
        }

        private string show_save_dialog(string savename)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = savename; // Default file name
            dlg.DefaultExt = ".png"; // Default file extension
            dlg.Filter = "PNG (.png)|*.png|JPG (.jpg)|*.jpg"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                string filename = dlg.FileName;
                return filename;
            }
            return "";
        }

        private void Victim_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = show_save_dialog("Victim_Chart");
                if (path == "")
                {
                    throw new Exception("Đường dẫn không hợp lệ");
                }
                SaveToPng(VictimChart, path);
                new MessageBoxCustom("Thành công", "Xuất biểu đồ thành công!", MessageType.Success, MessageButtons.OK).ShowDialog();
            }
            catch (Exception ex)
            {
                new MessageBoxCustom("Lỗi", ex.Message, MessageType.Error, MessageButtons.OKCancel).ShowDialog();
            }
        }
        private void Doctor_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = show_save_dialog("Doctor_Chart");
                if (path == "")
                {
                    throw new Exception("Đường dẫn không hợp lệ");
                }
                SaveToPng(DoctorChart, path);
                new MessageBoxCustom("Thành công", "Xuất biểu đồ thành công!", MessageType.Success, MessageButtons.OK).ShowDialog();
            }
            catch (Exception ex)
            {
                new MessageBoxCustom("Lỗi", ex.Message, MessageType.Error, MessageButtons.OKCancel).ShowDialog();
            }
        }
        private void Service_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string path = show_save_dialog("Income_Chart");
                if (path == "")
                {
                    throw new Exception("Đường dẫn không hợp lệ");
                }
                SaveToPng(MoneyChart, path);
                new MessageBoxCustom("Thành công", "Xuất biểu đồ thành công!", MessageType.Success, MessageButtons.OK).ShowDialog();
            }
            catch (Exception ex)
            {
                new MessageBoxCustom("Lỗi", ex.Message, MessageType.Error, MessageButtons.OKCancel).ShowDialog();
            }
        }
        private void SaveToPng(FrameworkElement visual, string fileName)
        {
            var encoder = new PngBitmapEncoder();
            EncodeVisual(visual, fileName, encoder);
        }

        private static void EncodeVisual(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        {
            var bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            var frame = BitmapFrame.Create(bitmap);
            encoder.Frames.Add(frame);
            using (var stream = File.Create(fileName)) encoder.Save(stream);
        }
    }
}
