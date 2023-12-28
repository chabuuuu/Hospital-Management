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
using System.Globalization;
using System.Net.NetworkInformation;

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
        private string tenkhoa;
        private string bacsi;
        private string ngaythanhlap;
        private List<string> bacsiList;


        public ICommand CancelCommand { get; }
        public ICommand ConfirmAddCommand { get; }
        public ICommand AddImageCommand { get; }
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

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

        public string Tenkhoa
        {
            get => tenkhoa; set
            {
                tenkhoa = value;
                OnPropertyChanged(nameof(Tenkhoa));
            }
        }
        public string Bacsi
        {
            get => bacsi; set
            {
                bacsi = value;
                OnPropertyChanged(nameof(Bacsi));
            }
        }
        public List<string> BacsiList
        {
            get => bacsiList; set
            {
                bacsiList = value;
                OnPropertyChanged(nameof(BacsiList));
            }
        }

        public string Ngaythanhlap
        {
            get => ngaythanhlap; set
            {
                ngaythanhlap = value;
                OnPropertyChanged(nameof(Ngaythanhlap));
            }
        }
        public void loadBacsi()
        {
            List<YSI> bacsi = _db.YSI.ToList();
            List<String> subID = new List<String>();
            foreach (var item in bacsi)
            {
                if (item.LOAIYSI == null)
                {
                    continue;
                }
                if (item.LOAIYSI.Substring(0, 6).Equals("Bác sĩ"))
                {
                    //Bỏ qua các Bác Sĩ đã là trưởng khoa rồi
                    if (item.KHOA.TRUONGKHOA == item.MAYSI)
                    {
                        continue;
                    }
                    subID.Add(item.SUB_ID + ": " + item.HOTEN);
                }
            }
            this.BacsiList = subID;

        }
        public int convertBacsiSub_ID(string inputString)
        {
            // Tách chuỗi sử dụng phương thức Split
            string[] parts = inputString.Split(new[] { ':' }, 2);
            string k1 = parts[0].Substring(1);
            return int.Parse(k1);
        }
        public void insert()
        {
            KHOA newKhoa = new KHOA()
            {
                TENKHOA = Tenkhoa,
                NGAYTHANHLAP = DateTime.ParseExact(Ngaythanhlap, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture),
                //TRUONGKHOA = convertBacsiSub_ID(Bacsi),
                PICTURE = Image_url,
            };
            _db.KHOA.AddObject(newKhoa);
            _db.SaveChanges();
        }

        public AddFieldViewModel()
        {
            //loadBacsi();
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
           // try
           // {
                postImage(store_dir);
                new MessageBoxCustom("Thông báo", "Thêm ảnh thành công", MessageType.Success, MessageButtons.OK).ShowDialog();

           /* }
            catch (Exception e)
            {
                new MessageBoxCustom("Lỗi", "Thêm ảnh thất bại\n Lỗi: " + e.Message, MessageType.Error, MessageButtons.OK).ShowDialog();
            } 
           */
        }

        private bool CanExecuteAddCommand(object? obj)
        {
            return true; //dk: không trùng với cái đã có
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
