using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using LTTQ_DoAn.Repositories;
using LTTQ_DoAn.Model;
using FontAwesome.Sharp;
using System.Windows;
using Newtonsoft.Json;
using static LTTQ_DoAn.ViewModel.LoginViewModel;
using System.IO;
using System.Web.UI.WebControls;
using LTTQ_DoAn.View;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LTTQ_DoAn.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private MainWindow main_wd;
        public static TAIKHOAN _currentUserAccount;
        private BaseViewModel _currentChildView;
        private string _caption;
        private IconChar _icon;
        private IUserRepository userRepository;
        private bool _isViewVisible = true;
        private bool roomVisibility = true;
        private bool appointmentVisibility = true;
        private bool victimVisibility = true;
        private bool fieldVisibility = true;
        private bool doctorAndNurseVisibility = true;
        private bool serviceVisibility = true;
        private bool medicineVisibility = true;
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));

            }
        }


        public TAIKHOAN CurrentUserAccount
        {
            get
            {
                return _currentUserAccount;
            }

            set
            {
                _currentUserAccount = value;
                OnPropertyChanged(nameof(CurrentUserAccount));
            }
        }

        public BaseViewModel CurrentChildView
        {
            get { return _currentChildView; }
            set { _currentChildView = value; OnPropertyChanged(nameof(CurrentChildView)); }
        }
        public string Caption 
        { 
            get { return _caption; }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowCustomerViewCommand {  get; }
        public ICommand ShowVictimViewCommand { get; }
        public ICommand ShowAppointmentViewCommand { get; }
        public ICommand ShowDoctorViewCommand { get; }
        public ICommand ShowServicesViewCommand { get; }
        public ICommand ShowFieldViewCommand { get; }
        public ICommand ShowRoomViewCommand { get; }
        public ICommand ShowMedicineViewCommand { get; }
        public ICommand LogoutViewCommand { get; }

        public MainWindow Main_wd { get => main_wd; set => main_wd = value; }
        void Set_doctor()
        {
            RoomVisibility = true;
            AppointmentVisibility = true;
            VictimVisibility = true;
            FieldVisibility = true;
            DoctorAndNurseVisibility = true;
            ServiceVisibility = true;
            MedicineVisibility = true;
        }        
        void Set_admin()
        {
            RoomVisibility = true;
            AppointmentVisibility = true;
            VictimVisibility = true;
            FieldVisibility = true;
            DoctorAndNurseVisibility = true;
            ServiceVisibility = true;
            MedicineVisibility = true;
        }        
        void Set_staff()
        {
            RoomVisibility = true;
            AppointmentVisibility = true;
            VictimVisibility = true;
            FieldVisibility = true;
            DoctorAndNurseVisibility = true;
            ServiceVisibility = true;
            MedicineVisibility = true;
        }
        void Set_permission(string type)
        {
            switch (type)
            {
                case "Admin":
                    Set_admin();
                    break;
                case "Staff":
                    Set_staff();
                    break;
                case "Doctor":
                    Set_doctor();
                    break;
                default:
                    break;
            }
        }

        public bool RoomVisibility
        {
            get => roomVisibility; set
            {
                roomVisibility = value;
                OnPropertyChanged(nameof(RoomVisibility));
            }
        }
        public bool AppointmentVisibility
        {
            get => appointmentVisibility; set
            {
                appointmentVisibility = value;
                OnPropertyChanged(nameof(AppointmentVisibility));
            }
        }
        public bool VictimVisibility
        {
            get => victimVisibility; set
            {
                victimVisibility = value;
                OnPropertyChanged(nameof (VictimVisibility));
            }
        }
        public bool FieldVisibility
        {
            get => fieldVisibility; set
            {
                fieldVisibility = value;
                OnPropertyChanged(nameof(FieldVisibility));
            }
        }
        public bool DoctorAndNurseVisibility
        {
            get => doctorAndNurseVisibility; set
            {
                doctorAndNurseVisibility = value;
                OnPropertyChanged(nameof(DoctorAndNurseVisibility));
            }
        }
        public bool ServiceVisibility
        {
            get => serviceVisibility; set
            {
                serviceVisibility = value;
                OnPropertyChanged(nameof(ServiceVisibility));
            }
        }
        public bool MedicineVisibility
        {
            get => medicineVisibility; set
            {
                medicineVisibility = value;
                OnPropertyChanged(nameof(MedicineVisibility));
            }
        }
        //Dùng cái này
        public MainViewModel(MainWindow wd, TAIKHOAN user_account)
        {
            Main_wd = wd;
            CurrentUserAccount = user_account;
            Set_permission(CurrentUserAccount.LOAITAIKHOAN);
            userRepository = new UserRepository();
            //CurrentUserAccount = new UserAccountModel();
            //khởi tạo phương thức xem view
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);
            ShowVictimViewCommand = new ViewModelCommand(ExecuteShowVictimViewCommand);
            ShowAppointmentViewCommand = new ViewModelCommand(ExecuteShowAppoinmentViewCommand);
            ShowDoctorViewCommand = new ViewModelCommand(ExecuteShowDoctorViewCommand);
            ShowRoomViewCommand = new ViewModelCommand(ExecuteShowRoomViewCommand);
            ShowFieldViewCommand = new ViewModelCommand(ExecuteShowFieldViewCommand);
            ShowServicesViewCommand = new ViewModelCommand(ExecuteShowServicesViewCommand);
            ShowMedicineViewCommand = new ViewModelCommand(ExecuteShowMedicineViewCommand);
            LogoutViewCommand = new ViewModelCommand(ExecuteLogoutViewCommand);
            //Khởi tạo màn hình mặc định
            ExecuteShowHomeViewCommand(new object());
            //LoadCurrentUserData();
        }
        public MainViewModel()
        {
            userRepository = new UserRepository();
            //CurrentUserAccount = new UserAccountModel();
            //khởi tạo phương thức xem view
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);
            ShowVictimViewCommand = new ViewModelCommand(ExecuteShowVictimViewCommand);
            ShowAppointmentViewCommand = new ViewModelCommand(ExecuteShowAppoinmentViewCommand);
            ShowDoctorViewCommand = new ViewModelCommand(ExecuteShowDoctorViewCommand);
            ShowRoomViewCommand = new ViewModelCommand(ExecuteShowRoomViewCommand);
            ShowFieldViewCommand = new ViewModelCommand(ExecuteShowFieldViewCommand);
            ShowServicesViewCommand = new ViewModelCommand(ExecuteShowServicesViewCommand);
            ShowMedicineViewCommand = new ViewModelCommand(ExecuteShowMedicineViewCommand);
            LogoutViewCommand = new ViewModelCommand(ExecuteLogoutViewCommand);
            //Khởi tạo màn hình mặc định
            ExecuteShowHomeViewCommand(new object());
            //LoadCurrentUserData();
        }
        public class Account
        {
            public string username;
            public string password;
        }
        private void DeleteJsonData()
        {
            // Tạo một đối tượng chứa dữ liệu bạn muốn lưu
            Account account = new Account();
            account.username = "";
            account.password = "";
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            //MessageBox.Show(currentDirectory);
            //string direct = @"D:\\Workspace\\Learn\\TestGitDoAn\\image.png";
            string store_dir = currentDirectory + "\\account.json";
            // Đường dẫn đến file JSON (thay đổi đường dẫn theo nhu cầu của bạn)
            //string filePath = "path/to/your/file.json";

            // Sử dụng thư viện Newtonsoft.Json để chuyển đối tượng thành chuỗi JSON
            string jsonData = JsonConvert.SerializeObject(account, Formatting.Indented);

            // Lưu chuỗi JSON xuống file
            File.WriteAllText(store_dir, jsonData);

            //MessageBox.Show("Dữ liệu đã được lưu xuống file JSON!");
        }
        private void ExecuteLogoutViewCommand(object obj)
        {
            DeleteJsonData();
            new MessageBoxCustom("Đăng xuất thành công", "Hẹn gặp lại", MessageType.Success, MessageButtons.OK).ShowDialog();
            Main_wd.Close();
            LoginWindow wd = new LoginWindow();
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }

        private void ExecuteShowCustomerViewCommand(object obj)
        {
            CurrentChildView = new CustomerViewModel();
            Caption = "Customers";
            Icon = IconChar.UserGroup;
        }

        private void ExecuteShowHomeViewCommand(object obj)
        {
            CurrentChildView = new HomeViewModel();
            Caption = "Dashboard";
            Icon = IconChar.Home;
        }

        private void ExecuteShowVictimViewCommand(object obj)
        {
            CurrentChildView = new VictimViewModel();
            Caption = "Bệnh nhân";
            Icon = IconChar.Bed;
        }   
        private void ExecuteShowAppoinmentViewCommand(object obj)
        {
            CurrentChildView = new AppointmentViewModel();
            Caption = "Lịch khám";
            Icon = IconChar.CalendarDays;
        }
        private void ExecuteShowDoctorViewCommand(object obj)
        {
            CurrentChildView = new DoctorAndNurseViewModel();
            Caption = "Y sĩ";
            Icon = IconChar.UserNurse;
        }
        private void ExecuteShowRoomViewCommand(object obj)
        {
            CurrentChildView = new RoomViewModel();
            Caption = "Phòng bệnh";
            Icon = IconChar.CirclePlus;
        }
        private void ExecuteShowFieldViewCommand(object obj)
        {
            CurrentChildView = new FieldViewModel();
            Caption = "Khoa";
            Icon = IconChar.Clone;
        }
        private void ExecuteShowServicesViewCommand(object obj)
        {
            CurrentChildView = new ServicesViewModel();
            Caption = "Dịch vụ";
            Icon = IconChar.ArrowDownAZ;
        }
        private void ExecuteShowMedicineViewCommand(object obj)
        {
            CurrentChildView = new MedicineViewModel();
            Caption = "Thuốc";
            Icon = IconChar.Pills;
        }


        /*private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentUserAccount.Username = user.Username;
                CurrentUserAccount.DisplayName = $"Welcome {user.Name} {user.LastName} ;)";
                CurrentUserAccount.ProfilePicture = null;
            }
            else
            {
                CurrentUserAccount.DisplayName = "Invalid user, not logged in";
                //Hide child views.
            }
        }*/
    }
}
