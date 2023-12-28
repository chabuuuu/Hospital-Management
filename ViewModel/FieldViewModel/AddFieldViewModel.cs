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
using Microsoft.Win32;
using RestSharp;
using System.Xml.Linq;
using System.Text.Json.Serialization;
using Json.Net;
using LTTQ_DoAn.View;

namespace LTTQ_DoAn.ViewModel
{
    public class ImageResponse
    {
        public string status;
        public string url;
    }
    public class AddFieldViewModel : BaseViewModel
    {
        private BitmapImage image;
        private string image_url;
        public ICommand CancelCommand { get; }
        public ICommand ConfirmAddCommand { get; }
        public ICommand AddImageCommand { get; }
        public BitmapImage Image { get => image; set
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        public string Image_url { get => image_url; set
            {
                image_url = value;
                OnPropertyChanged(nameof(Image_url));
            }
        }

        public AddFieldViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmAddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            AddImageCommand = new ViewModelCommand(ExecuteAddImageCommand, CanExecuteAddImageCommand);
        }

        private bool CanExecuteCancelCommand(object? obj)
        {
            return true; //ko điều kiện
        }
        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }

        private bool CanExecuteAddImageCommand(object? obj)
        {
            return true; //ko điều kiện
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
            //MessageBox.Show(currentDirectory);
            //string direct = @"D:\\Workspace\\Learn\\TestGitDoAn\\image.png";
            string store_dir = currentDirectory + "\\field_temporary_image.png";
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(Image));

            using (var fileStream = new System.IO.FileStream(store_dir, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
            try
            {
                postImage(store_dir);

            }
            catch (Exception e)
            {
                new MessageBoxCustom("Lỗi", "Thêm ảnh thất bại", MessageType.Error, MessageButtons.OK).ShowDialog();
            }
        }

        private bool CanExecuteAddCommand(object? obj)
        {
            return true; //dk: không trùng với cái đã có
        }
        
        private void postImage(string path)
        {
            var client = new RestClient("https://quanlybenhvien.onrender.com");
            // client.Authenticator = new HttpBasicAuthenticator(username, password);
            var request = new RestRequest("photo/item");
            request.AddFile("image", path);
            var response = client.Post(request);
            var content = response.Content; // Raw content as string
            ImageResponse jsonResponse = JsonNet.Deserialize<ImageResponse>(content);
            string data = jsonResponse.url.ToString();
            Image_url = data;
            new MessageBoxCustom("Thông báo", "Thêm ảnh thành công", MessageType.Success, MessageButtons.OK).ShowDialog();
        }
        
        private void ExecuteAddCommand(object? obj)
        {



            Application.Current.MainWindow.Close();
        }
    }
}
