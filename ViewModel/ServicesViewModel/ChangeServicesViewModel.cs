using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using Json.Net;
using LTTQ_DoAn.View;
using RestSharp;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Net.NetworkInformation;

namespace LTTQ_DoAn.ViewModel
{
    public class ChangeServicesViewModel : BaseViewModel
    {
        private DICHVU dichvu;
        private BitmapImage image;
        private string image_url;
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

        public ICommand CancelCommand { get; }
        public ICommand ConfirmChangeCommand { get; }
        public ICommand AddImageCommand { get; }

        public DICHVU Dichvu
        {
            get => dichvu; set
            {
                dichvu = value;
                OnPropertyChanged(nameof(Dichvu));
            }
        }

        public BitmapImage Image
        {
            get => image; set
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }
        public string Image_url
        {
            get => image_url; set
            {
                image_url = value;
                OnPropertyChanged(nameof(Image_url));
            }
        }
        private void update()
        {
            DICHVU updateDichvu = (from m in _db.DICHVU
                                       where m.MADICHVU == Dichvu.MADICHVU
                                       select m).Single();
            updateDichvu.TENDICHVU = Dichvu.TENDICHVU;
            updateDichvu.GIATIEN = Dichvu.GIATIEN;
            updateDichvu.PICTURE = Image_url;
            _db.SaveChanges();
        }
        public ChangeServicesViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            AddImageCommand = new ViewModelCommand(ExecuteAddImageCommand, CanExecuteAddImageCommand);

        }
        public ChangeServicesViewModel(DICHVU SelectedDichVu)
        {
            Dichvu = SelectedDichVu;
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            AddImageCommand = new ViewModelCommand(ExecuteAddImageCommand, CanExecuteAddImageCommand);

        }
        private bool CanExecuteAddImageCommand(object? obj)
        {
            return true; //ko điều kiện
        }
        private void postImage(string path)
        {
            var client = new RestClient("http://3.25.245.200");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest("photo/item");
            request.AddFile("image", path);
            var response = client.Post(request);
            var content = response.Content; // Raw content as string
            ImageResponse jsonResponse = JsonNet.Deserialize<ImageResponse>(content);
            string data = jsonResponse.url.ToString();
            Image_url = data;
        }
        private void ExecuteAddImageCommand(object? obj)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image files|*.bmp;*.jpg;*.png";
            ofd.FilterIndex = 1;
            if (ofd.ShowDialog() == true)
            {
                Image = new BitmapImage(new Uri(ofd.FileName));
            }
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            string store_dir = currentDirectory + "\\service_temporary_image.png";
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(Image));

            using (var fileStream = new System.IO.FileStream(store_dir, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
            try
            {
                postImage(store_dir);
                new MessageBoxCustom("Thông báo", "Tải ảnh lên thành công", MessageType.Success, MessageButtons.OK).ShowDialog();

            }

            catch (Exception e)
            {
                new MessageBoxCustom("Lỗi", "Tải ảnh lên thất bại\n Lỗi: " + e.Message, MessageType.Error, MessageButtons.OK).ShowDialog();
            }

        }
        private bool CanExecuteCancelCommand(object? obj)
        {
            return true; //ko điều kiện
        }
        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
        private bool CanExecuteChangeCommand(object? obj)
        {
            return true; //dk: thay đổi xong không trùng với khoa đã có
        }
        private void ExecuteChangeCommand(object? obj)
        {
            try
            {
                update();
                //MessageBox.Show("Sửa thông tin y sĩ thành công!");
                new MessageBoxCustom("Thành công", "Sửa thông tin dịch vụ thành công!", MessageType.Success, MessageButtons.OK).ShowDialog();
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ

            }
            catch (Exception err)
            {
                new MessageBoxCustom("Lỗi", err.Message, MessageType.Error, MessageButtons.OKCancel).ShowDialog();
                //MessageBox.Show(err.Message);
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ
            }
        }
    }
}
