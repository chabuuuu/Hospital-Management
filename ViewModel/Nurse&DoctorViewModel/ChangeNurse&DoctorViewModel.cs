using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using LTTQ_DoAn.View;

namespace LTTQ_DoAn.ViewModel
{
    public class ChangeNurseAndDoctorViewModel : BaseViewModel
    {
        private YSI ysi = null;
        private string phong_subid;
        private string khoa = null;
        private string chihuy = null;
        private List<String> gender = new List<string>() { "Nam", "Nữ" };
        private List<String> khoaList;
        public List<String> KhoaList
        {
            get => khoaList; set
            {
                khoaList = value;
                OnPropertyChanged(nameof(KhoaList));
            }
        }
        private List<String> ysiList;
        public void loadKhoa()
        {
            List<KHOA> khoa = _db.KHOA.ToList();
            List<String> subID = new List<String>();
            foreach (var item in khoa)
            {
                subID.Add(item.SUB_ID + ": " + item.TENKHOA);
                if (item.MAKHOA == Ysi.MAKHOA)
                {
                    Khoa = item.SUB_ID + ": " + item.TENKHOA;
                }
            }
            this.KhoaList = subID;
        }
        public void loadYsi()
        {
            List<YSI> ysi = _db.YSI.ToList();
            List<String> subID = new List<String>();
            foreach (var item in ysi)
            {
                subID.Add(item.SUB_ID + ": " + item.HOTEN);
                if (item.MAYSI == Ysi.MACHIHUY && Ysi.MACHIHUY != null)
                {
                    Chihuy = item.SUB_ID + ": " + item.HOTEN;
                }
            }
            if (Ysi.MACHIHUY == null)
            {
                Chihuy = "Không có cấp trên";
            }
            subID.Add("Không có cấp trên");
            this.YsiList = subID;
        }
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        public ICommand CancelCommand { get; }
        public ICommand ConfirmChangeCommand { get; }
        public YSI Ysi { get => ysi; set
            {
                ysi = value;
                OnPropertyChanged(nameof(Ysi));
            }
        }

        public string Phong_subid { get => phong_subid; set
            {
                phong_subid = value;
                OnPropertyChanged(Phong_subid);
            }
        }

        public string Khoa { get => khoa; set
            {
                khoa = value;
                OnPropertyChanged(nameof(Khoa));
            }
        }

        public List<string> YsiList { get => ysiList; set
            {
                ysiList = value;
            }
        }

        public string Chihuy { get => chihuy; set
            {
                chihuy = value;
                OnPropertyChanged(nameof(Chihuy));
            }
        }

        public List<string> Gender { get => gender; set => gender = value; }

        public ChangeNurseAndDoctorViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteConfirmChangeCommand, CanExecuteConfirmChangeCommand);
        }
        public ChangeNurseAndDoctorViewModel(YSI SelectedYSi)
        {
            Ysi = SelectedYSi;
            Phong_subid = "PHG" + SelectedYSi.MAPHONG.ToString();
            loadKhoa();
            loadYsi();
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteConfirmChangeCommand, CanExecuteConfirmChangeCommand);
        }
        private int? convertPhongSUB_ID(string Sub_id)
        {
            if (Sub_id == null)
            {
                return null;
            }
            // Chuỗi cần tách
            string inputString = Sub_id;

            // Tách các ký tự còn lại thành một chuỗi riêng
            string remainingCharacters = inputString.Substring(3);

            return int.Parse(remainingCharacters);
        }
        public int convertKhoaSub_ID(string inputString)
        {
            // Tách chuỗi sử dụng phương thức Split
            string[] parts = inputString.Split(new[] { ':' }, 2);
            string k1 = parts[0].Substring(1);
            return int.Parse(k1);
        }
        public int? convertChiHuySUB_ID(string Sub_id)
        {
            if (Sub_id == null || string.Compare(Sub_id, "Không có cấp trên") == 0)
            {
                return null;
            }
            // Chuỗi cần tách

            string[] parts = Sub_id.Split(new[] { ':' }, 2);
            string k1 = parts[0].Substring(1);
            return int.Parse(k1);
        }
        private void update()
        {
            YSI updateYsi = (from m in _db.YSI
                                       where m.MAYSI == Ysi.MAYSI
                                       select m).Single();
            updateYsi.HOTEN = Ysi.HOTEN;
            updateYsi.MAPHONG = convertPhongSUB_ID(Phong_subid);
            updateYsi.GIOITINH = Ysi.GIOITINH;
            updateYsi.NGAYSINH = Ysi.NGAYSINH;
            updateYsi.NGAYVAOLAM = Ysi.NGAYVAOLAM;
            updateYsi.MAKHOA = convertKhoaSub_ID(Khoa);
            updateYsi.LOAIYSI = Ysi.LOAIYSI;
            updateYsi.MACHIHUY = convertChiHuySUB_ID(Chihuy);
            _db.SaveChanges();
        }
        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
        //điều kiện để lệnh hủy bỏ được thực hiện: k có điều kiện
        private bool CanExecuteCancelCommand(object? obj)
        {
            return true;
        }
        //---------------------------------------------
        private void ExecuteConfirmChangeCommand(object? obj)
        {
            try
            {
                update();
                //MessageBox.Show("Sửa thông tin y sĩ thành công!");
                new MessageBoxCustom("Thành công", "Sửa thông tin y sĩ thành công!", MessageType.Success, MessageButtons.OK).ShowDialog();
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ

            }
            catch (Exception err)
            {
                new MessageBoxCustom("Lỗi", err.Message, MessageType.Error, MessageButtons.OKCancel).ShowDialog();
                //MessageBox.Show(err.Message);
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ
            }
        }
        private bool CanExecuteConfirmChangeCommand(object? obj)
        {
            return true;
        }

    }
}
