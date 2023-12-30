using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using System.Windows.Forms;
using LTTQ_DoAn.View;
using System.Windows.Media.Imaging;
using Json.Net;
using RestSharp;
using System.Net.NetworkInformation;

namespace LTTQ_DoAn.ViewModel
{
    public class ChangeFieldViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

        private string bacsi = null;
        private string image_url;
        private BitmapImage image;
        private List<string> bacsiList;
        private KHOA khoa;
        public ICommand CancelCommand { get; }
        public ICommand ConfirmChangeCommand { get; }
        public ICommand AddImageCommand { get; }

        public KHOA Khoa
        {
            get => khoa; set
            {
                khoa = value;
                OnPropertyChanged(nameof(Khoa));
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

        public void loadBacsi()
        {
            List<YSI> bacsi = _db.YSI.ToList();
            List<String> subID = new List<String>();
            foreach (var item in bacsi)
            {
                if (item.LOAIYSI == null || item.MAKHOA != Khoa.MAKHOA)
                {
                    continue;
                }
                if (item.LOAIYSI.Substring(0, 6).Equals("Bác sĩ"))
                {
                    //Bỏ qua các Bác Sĩ đã là trưởng khoa rồi (trừ trưởng khoa của Khoa hiện tại)
                    if (item.KHOA.TRUONGKHOA == item.MAYSI && item.MAYSI != Khoa.TRUONGKHOA)
                    {
                        continue;
                    }
                    subID.Add(item.SUB_ID + ": " + item.HOTEN);
                }
            }
            this.BacsiList = subID;
        }
        private void FindTruongKhoa()
        {
            if (Khoa.TRUONGKHOA == null)
            {
                return;
            }
                string? tenBacSi = (from m in _db.YSI
                                    where m.MAYSI == Khoa.TRUONGKHOA
                                    select m.HOTEN).First();
                if (tenBacSi == null)
                {
                    return;
                }
                Bacsi = "M" + Khoa.TRUONGKHOA.ToString() + ": " + tenBacSi;
        }
        public ChangeFieldViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            AddImageCommand = new ViewModelCommand(ExecuteAddImageCommand, CanExecuteAddImageCommand);

        }
        public int convertBacsiSub_ID(string inputString)
        {
            // Tách chuỗi sử dụng phương thức Split
            string[] parts = inputString.Split(new[] { ':' }, 2);
            string k1 = parts[0].Substring(1);
            return int.Parse(k1);
        }
        private void update()
        {
            KHOA updateKhoa = (from m in _db.KHOA
                                       where m.MAKHOA == Khoa.MAKHOA
                                       select m).Single();
            updateKhoa.TENKHOA = Khoa.TENKHOA;
            updateKhoa.NGAYTHANHLAP = Khoa.NGAYTHANHLAP;
            if (Bacsi != null)
            {
                updateKhoa.TRUONGKHOA = convertBacsiSub_ID(Bacsi);
            }
            updateKhoa.PICTURE = Image_url;
            _db.SaveChanges();
        }
        public ChangeFieldViewModel(KHOA SelectedKhoa)
        {
            Khoa = SelectedKhoa;
            loadBacsi();
            FindTruongKhoa();
            Image_url = SelectedKhoa.PICTURE;
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
            if (ofd.ShowDialog() == DialogResult.OK)
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
                new MessageBoxCustom("Thông báo", "Tải ảnh thành công", MessageType.Success, MessageButtons.OK).ShowDialog();

            }

            catch (Exception e)
            {
                new MessageBoxCustom("Lỗi", "Tải ảnh thất bại\n Lỗi: " + e.Message, MessageType.Error, MessageButtons.OK).ShowDialog();
            }

        }
        private bool CanExecuteCancelCommand(object? obj)
        {
            return true; //ko điều kiện
        }
        private void ExecuteCancelCommand(object? obj)
        {
            System.Windows.Application.Current.MainWindow.Close();
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
                new MessageBoxCustom("Thành công", "Sửa thông tin khoa thành công!", MessageType.Success, MessageButtons.OK).ShowDialog();
                System.Windows.Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ

            }
            catch (Exception err)
            {
                new MessageBoxCustom("Lỗi", err.Message, MessageType.Error, MessageButtons.OKCancel).ShowDialog();
                //MessageBox.Show(err.Message);
                System.Windows.Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ
            }
        }
    }
}
