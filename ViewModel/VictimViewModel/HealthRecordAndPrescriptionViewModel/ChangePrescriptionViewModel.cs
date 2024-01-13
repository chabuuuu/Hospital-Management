using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using LTTQ_DoAn.View;

namespace LTTQ_DoAn.ViewModel
{
    public class ChangePrescriptionViewModel : BaseViewModel
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        private DONTHUOC donthuoc;
        private string tenbenhnhan;
        private string ghichu;
        private string benhan;
        private List<String> benhanlist;
        public List<String> BenhAnList
        {
            get => benhanlist; set
            {
                benhanlist = value;
                OnPropertyChanged(nameof(BenhAnList));
            }
        }
        public string Benhan
        {
            get => benhan; set
            {
                benhan = value;
                OnPropertyChanged(nameof(Benhan));
            }
        }
        public ICommand CancelCommand { get; }
        public ICommand ConfirmChangeCommand { get; }
        public DONTHUOC Donthuoc
        {
            get => donthuoc; set
            {
                donthuoc = value;
                OnPropertyChanged(nameof(Donthuoc));
            }
        }

        public string Tenbenhnhan
        {
            get => tenbenhnhan; set
            {
                tenbenhnhan = value;
                OnPropertyChanged(nameof(Tenbenhnhan));
            }
        }
        public string Ghichu
        {
            get => ghichu; set
            {
                ghichu = value;
                OnPropertyChanged(nameof(Ghichu));
            }
        }

        public void loadBenhan()
        {
            //List<BENHAN> benhan = _db.BENHAN.ToList();
            List<BENHAN> benhan = Donthuoc.BENHAN.BENHNHAN.BENHAN.ToList();
            List<String> subID = new List<String>();
            foreach (var item in benhan)
            {
                subID.Add(item.SUB_ID);
            }
            this.BenhAnList = subID;
            Benhan = Donthuoc.SUB_ID;
        }
        public ChangePrescriptionViewModel()
        {
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteConfirmChangeCommand, CanExecuteConfirmChangeCommand);
        }
        private void findDonThuoc(int maDonThuoc)
        {

            DONTHUOC donThuoc = (from m in _db.DONTHUOC
                                  where m.MADONTHUOC == maDonThuoc
                                 select m).First();
            Donthuoc = donThuoc;
            
        }
        public int convertBenhanSub_ID(string inputString)
        {
            //string[] parts = inputString.Split(new[] { ':' }, 2);
            string k1 = inputString.Substring(2);
            return int.Parse(k1);
        }
        private void update()
        {
            DONTHUOC updateDonthuoc = (from m in _db.DONTHUOC
                                   where m.MADONTHUOC == Donthuoc.MADONTHUOC
                                       select m).Single();
            updateDonthuoc.MABENHAN = convertBenhanSub_ID(Benhan);
            updateDonthuoc.GHICHU = Ghichu;
            _db.SaveChanges();
        }
        public ChangePrescriptionViewModel(int maDonThuoc)
        {
            findDonThuoc(maDonThuoc);
            loadBenhan();
            Tenbenhnhan = Donthuoc.BENHAN.BENHNHAN.HOTEN;
            Ghichu = Donthuoc.GHICHU;
            Benhan = Donthuoc.BENHAN.SUB_ID;
            CancelCommand = new ViewModelCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
            ConfirmChangeCommand = new ViewModelCommand(ExecuteConfirmChangeCommand, CanExecuteConfirmChangeCommand);
        }
        private bool CanExecuteCancelCommand(object? obj)
        {
            return true;
        }
        private void ExecuteCancelCommand(object? obj)
        {
            Application.Current.MainWindow.Close();
        }
        private bool CanExecuteConfirmChangeCommand(object? obj)
        {
            return true;
        }
        private void ExecuteConfirmChangeCommand(object? obj)
        {
            try
            {
                update();
                new MessageBoxCustom("Thông báo", "Sửa thông tin đơn thuốc thành công!",
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
    }
}
