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

namespace LTTQ_DoAn.ViewModel
{
    public class ChangeFieldViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

        private string bacsi;
        private List<string> bacsiList;
        private KHOA khoa;
        public ICommand CancelCommand { get; }
        public ICommand ConfirmChangeCommand { get; }
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

        }
        public ChangeFieldViewModel(KHOA SelectedKhoa)
        {
            Khoa = SelectedKhoa;
            loadBacsi();
            FindTruongKhoa();
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);

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
            //
            System.Windows.Application.Current.MainWindow.Close();
        }
    }
}
