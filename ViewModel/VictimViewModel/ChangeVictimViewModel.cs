using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class ChangeVictimViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        public ICommand CancelCommand { get; }
        public ICommand ConfirmChangeCommand { get; }
        public BENHNHAN Benhnhan { get => benhnhan; set
            {
                benhnhan = value;
                OnPropertyChanged(nameof(Benhnhan));
            }
        }

        public List<string> Gender { get => gender; set => gender = value; }
        public string Gioitinh { get => gioitinh; set
            {
                gioitinh = value;
                OnPropertyChanged(nameof(Gioitinh));
            }
        }

        public string Phong_SUBID { get => phong_SUBID; set
            {
                phong_SUBID = value;
                OnPropertyChanged(nameof(Phong_SUBID));
            }
        }

        private BENHNHAN benhnhan = null;
        private List<String> gender = new List<string>() { "Nam", "Nữ" };
        private string gioitinh;
        private string phong_SUBID;
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
        private void update()
        {
            BENHNHAN updateBenhnhan = (from m in _db.BENHNHAN
                                   where m.MABENHNHAN == Benhnhan.MABENHNHAN
                                   select m).Single();
            updateBenhnhan.HOTEN = Benhnhan.HOTEN;
            updateBenhnhan.MAPHONG = convertPhongSUB_ID(Phong_SUBID);
            updateBenhnhan.GIOITINH = Benhnhan.GIOITINH;
            updateBenhnhan.NGAYSINH = Benhnhan.NGAYSINH;
            updateBenhnhan.DIACHI = Benhnhan.DIACHI;
            updateBenhnhan.MABHYT = Benhnhan.MABHYT;
            updateBenhnhan.NGAYNHAPVIEN = Benhnhan.NGAYNHAPVIEN;
            //updateMember.gender = genderComboBox.Text;
            _db.SaveChanges();
        }
        public ChangeVictimViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteConfirmChangeCommand, CanExecuteConfirmChangeCommand);
        }
        public ChangeVictimViewModel(BENHNHAN SelectedBenhNhan)
        {
            Benhnhan = SelectedBenhNhan;
            Phong_SUBID = "PHG" + SelectedBenhNhan.MAPHONG.ToString();
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteConfirmChangeCommand, CanExecuteConfirmChangeCommand);
        }
        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
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
                //MessageBox.Show("Sửa thông tin bệnh nhân thành công!");
                new MessageBoxCustom("Thông báo", "Sửa thông tin bệnh nhân thành công!", 
                    MessageType.Success, 
                    MessageButtons.OK)
                    .ShowDialog();
                Application.Current.MainWindow.Close(); // sau khi thêm sẽ đóng cửa sổ

            }
            catch (Exception err)
            {
                new MessageBoxCustom("Lỗi", err.Message,
                    MessageType.Error,
                    MessageButtons.OK)
                    .ShowDialog();
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
