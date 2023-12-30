using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using System.Windows.Media.Imaging;
using LTTQ_DoAn.View;
using Json.Net;
using RestSharp;
using Microsoft.Win32;
using System.Globalization;

namespace LTTQ_DoAn.ViewModel
{
    public class AddServicesViewModel : BaseViewModel
    {
        private BitmapImage image;
        private string image_url;
        private string tendichvu;
        private string gia;
        private List<string> dichvuList;
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

        public ICommand CancelCommand { get; }
        public ICommand ConfirmAddCommand { get; }
        public ICommand AddImageCommand { get; }

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
        public string Tendichvu
        {
            get => tendichvu; set
            {
                tendichvu = value;
                OnPropertyChanged(nameof(Tendichvu));
            }
        }
        public string Gia
        {
            get => gia; set
            {
                gia = value;
                OnPropertyChanged(nameof(Gia));
            }
        }
        public List<string> DichvuList
        {
            get => dichvuList; set
            {
                dichvuList = value;
                OnPropertyChanged(nameof(DichvuList));
            }
        }

        public AddServicesViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmAddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
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
                new MessageBoxCustom("Thông báo", "Thêm ảnh thành công", MessageType.Success, MessageButtons.OK).ShowDialog();

            }

            catch (Exception e)
            {
                new MessageBoxCustom("Lỗi", "Thêm ảnh thất bại\n Lỗi: " + e.Message, MessageType.Error, MessageButtons.OK).ShowDialog();
            }

        }

        public void insert()
        {
            DICHVU newDichvu = new DICHVU()
            {
                TENDICHVU = Tendichvu,
                GIATIEN = Decimal.Parse(Gia),
                PICTURE = Image_url,
            };
            _db.DICHVU.AddObject(newDichvu);
            _db.SaveChanges();
        }

        private bool CanExecuteCancelCommand(object? obj)
        {
            return true; //ko dieu kien
        }

        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
        private bool CanExecuteAddCommand(object? obj)
        {
            return true; //dk: không trùng với cái đã có
        }

        private void ExecuteAddCommand(object? obj)
        {
            try
            {
                insert();
                new MessageBoxCustom("Thông báo", "Thêm khoa mới thành công", MessageType.Success, MessageButtons.OK).ShowDialog();
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ

            }
            catch (Exception err)
            {
                MessageBox.Show(err.InnerException.ToString());
                //new MessageBoxCustom("Lỗi", "Thêm khoa mới thất bại\nLỗi: " + err.Message, MessageType.Success, MessageButtons.OK).ShowDialog();
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ
            }
        }
    }
}
