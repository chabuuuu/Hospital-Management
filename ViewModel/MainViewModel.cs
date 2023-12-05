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
//using LTTQ_DoAn.Repositories;
//using LTTQ_DoAn.Model;
using FontAwesome.Sharp;
namespace LTTQ_DoAn.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        //private UserAccountModel _currentUserAccount;
        private BaseViewModel _currentChildView;
        private string _caption;
        private IconChar _icon;
        //private IUserRepository userRepository;

        /*
        public UserAccountModel CurrentUserAccount
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
        }*/

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


        public MainViewModel()
        {
            /*userRepository = new UserRepository();
            CurrentUserAccount = new UserAccountModel();*/

            //khởi tạo phương thức xem view
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowCustomerViewCommand = new ViewModelCommand(ExecuteShowCustomerViewCommand);
            ShowVictimViewCommand = new ViewModelCommand(ExecuteShowVictimViewCommand);
            ShowAppointmentViewCommand = new ViewModelCommand(ExecuteShowAppoinmentViewCommand);
            ShowDoctorViewCommand = new ViewModelCommand(ExecuteShowDoctorViewCommand);
            ShowRoomViewCommand = new ViewModelCommand(ExecuteShowRoomViewCommand);
            ShowFieldViewCommand = new ViewModelCommand(ExecuteShowFieldViewCommand);
            ShowServicesViewCommand = new ViewModelCommand(ExecuteShowServicesViewCommand);
            //Khởi tạo màn hình mặc định
            ExecuteShowHomeViewCommand(new object());
            //LoadCurrentUserData();
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
