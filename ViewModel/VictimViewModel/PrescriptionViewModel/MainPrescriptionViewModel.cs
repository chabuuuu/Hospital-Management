using LTTQ_DoAn.Model;
using LTTQ_DoAn.View;
using LTTQ_DoAn.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Forms;
using LTTQ_DoAn.ViewModell;
using System.Data.Entity.Infrastructure;

namespace LTTQ_DoAn.ViewModel
{
    public class DonThuocType
    {
        public int MADONTHUOC {  get; set; }
        public string SUB_ID { get; set; }
        public string GHICHU {  get; set; }
        public string MAYSI {  get; set; }
    }
    public class PrescriptionViewModel: BaseViewModel 
    {
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

        private HealthRecordAndPrescription thisView;
        public ICommand ChangeCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand DeleteCommand { get; }

        public ICommand ExitCommand { get; }
        public BENHNHAN Benhnhan
        {
            get => benhnhan; set
            {
                benhnhan = value;
                OnPropertyChanged(nameof(Benhnhan));
            }
        }

        public List<DonThuocType> Listdonthuoc
        {
            get =>listdonthuoc; set
            {
                listdonthuoc = value;
                OnPropertyChanged(nameof(Listdonthuoc));
            }
        }
        public DonThuocType Donthuoc
        {
            get => donthuoc; set
            {
                donthuoc = value;
                OnPropertyChanged(nameof(Donthuoc));
            }
        }
        private BENHNHAN benhnhan;
        private List<DonThuocType> listdonthuoc;
        private DonThuocType donthuoc;
        private void findDonThuoc()
        {
            _db = new QUANLYBENHVIENEntities();         
            List<DONTHUOC> query = (from m in _db.BENHAN
                                    join n in _db.DONTHUOC on m.MABENHAN equals n.MABENHAN
                                    where m.MABENHNHAN == Benhnhan.MABENHNHAN
                                    select n).ToList();

            List<DonThuocType> list = new List<DonThuocType>();
            foreach (var item in query)
            {
                DonThuocType benhan = new DonThuocType()
                {
                    MADONTHUOC = item.MADONTHUOC,
                    SUB_ID = item.SUB_ID,
                    GHICHU = item.GHICHU,
                    MAYSI = findYSi(item.MABENHAN),
                };
                list.Add(donthuoc);
            }
            Listdonthuoc = list;
        }
        private string findYSi(int? maBenhAn)
        {
            if (maBenhAn == null)
            {
                return "";
            }
            int? maYSi = (from m in _db.BENHAN
                          where m.MABENHAN == maBenhAn
                          select m.MAYSI).First();
            if (maYSi == null)
            {
                return "";
            }
            string? tenYSi = (from m in _db.YSI
                                where m.MAYSI == maYSi
                                select m.HOTEN).First();
            if (tenYSi == null)
            {
                return "";
            }
            return "M" + maYSi.ToString() + ": " + tenYSi;
        }

        public PrescriptionViewModel()
        {
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand, CanExecuteExitCommand);
        }
        public PrescriptionViewModel(BENHNHAN SelectedBenhNhan, HealthRecordAndPrescription view)
        {
            this.thisView = view;
            Benhnhan = SelectedBenhNhan;
            findDonThuoc();
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);
            DeleteCommand = new ViewModelCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            ChangeCommand = new ViewModelCommand(ExecuteChangeCommand, CanExecuteChangeCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand, CanExecuteExitCommand);
        }

        private bool CanExecuteAddCommand(object? obj)
        {
            return true;
        }
        private void ExecuteAddCommand(object? obj)
        {
            AddPrescription wd = new AddPrescription();
            wd.Closed += AddPrescription_Closed;
            wd.DataContext = new AddPrescriptionViewModel(Benhnhan);
            System.Windows.Application.Current.MainWindow = wd;
            wd.ShowDialog();

        }
        private void AddPrescription_Closed(object sender, EventArgs e)
        {
            findDonThuoc();
        }

        private bool CanExecuteDeleteCommand(object? obj)
        {
            return true;
        }
        private void ExecuteDeleteCommand(object? obj)
        {
            try
            {
                int Id = Donthuoc.MADONTHUOC;
                var deleteMember = _db.DONTHUOC.Where(m => m.MADONTHUOC == Id).Single();
                _db.DONTHUOC.DeleteObject(deleteMember);
                _db.SaveChanges();

                new MessageBoxCustom(
                    "Thông báo",
                    "Đã xóa đơn thuốc: " + Donthuoc.SUB_ID.ToString(),
                    MessageType.Success,
                    MessageButtons.OK)
                    .ShowDialog();
                findDonThuoc();
            }
            catch (Exception e)
            {
                new MessageBoxCustom(
                    "Thông báo",
                    e.Message + "\nLỗi: " + e.GetType().ToString(),
                    MessageType.Error,
                    MessageButtons.OK
                    )
                    .ShowDialog();
            }
        }

        private bool CanExecuteChangeCommand(object? obj)
        {
            return true;
        }
        private void ExecuteChangeCommand(object? obj)
        {
            if (Donthuoc == null)
            {
                return;
            }
            ChangeHealthRecord wd = new ChangeHealthRecord();
            wd.Closed += ChangePrescription_Closed;
            wd.DataContext = new ChangeHealthRecordViewModel(Donthuoc.MADONTHUOC);
            System.Windows.Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }

        private void ChangePrescription_Closed(object sender, EventArgs e)
        {
            
        }
        private bool CanExecuteExitCommand(object obj)
        {
            return true;
        }
        private void ExecuteExitCommand(object obj)
        {
            thisView.Close();
        }
    }
}
