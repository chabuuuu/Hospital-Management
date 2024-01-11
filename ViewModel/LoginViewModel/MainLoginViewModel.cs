using LTTQ_DoAn.View;
using Microsoft.Xaml.Behaviors.Media;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LTTQ_DoAn.ViewModel
{
    public static class SecurePasswordHasher
    {
        /// <summary>
        /// Size of salt.
        /// </summary>
        private const int SaltSize = 16;

        /// <summary>
        /// Size of hash.
        /// </summary>
        private const int HashSize = 20;

        /// <summary>
        /// Creates a hash from a password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="iterations">Number of iterations.</param>
        /// <returns>The hash.</returns>
        public static string Hash(string password, int iterations)
        {
            // Create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            // Create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            // Combine salt and hash
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            // Format hash with extra information
            return string.Format("$MYHASH$V1${0}${1}", iterations, base64Hash);
        }

        /// <summary>
        /// Creates a hash from a password with 10000 iterations
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>The hash.</returns>
        public static string Hash(string password)
        {
            return Hash(password, 10000);
        }

        /// <summary>
        /// Checks if hash is supported.
        /// </summary>
        /// <param name="hashString">The hash.</param>
        /// <returns>Is supported?</returns>
        public static bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$MYHASH$V1$");
        }

        /// <summary>
        /// Verifies a password against a hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashedPassword">The hash.</param>
        /// <returns>Could be verified?</returns>
        public static bool Verify(string password, string hashedPassword)
        {
            // Check hash
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }

            // Extract iteration and Base64 string
            var splittedHashString = hashedPassword.Replace("$MYHASH$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Get result
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
    public class LoginViewModel : BaseViewModel
    {
        //Fields
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private object executeloginCommand;

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));

            }
        }
        public bool IsViewVisible
        {
            get => _isViewVisible;
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));

            }
        }
        public class Account
        {
            public string username;
            public string password;
        }
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommnad { get; }
        public ICommand ShowPasswordCommnad { get; }
        public ICommand RememberPassword { get;  }
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        private void ReadFromJson()
        {
            // Đường dẫn đến file JSON cần đọc (thay đổi đường dẫn theo nhu cầu của bạn)
            string currentDirectory = System.IO.Directory.GetCurrentDirectory();
            //MessageBox.Show(currentDirectory);
            //string direct = @"D:\\Workspace\\Learn\\TestGitDoAn\\image.png";
            string store_dir = currentDirectory + "\\account.json";

            // Kiểm tra xem tệp có tồn tại không
            if (File.Exists(store_dir))
            {
                // Đọc nội dung từ tệp JSON
                string jsonData = File.ReadAllText(store_dir);

                // Deserialization (chuyển chuỗi JSON thành đối tượng)
                Account data = JsonConvert.DeserializeObject<Account>(jsonData);
                if (authenticate(data.username, data.password, false))
                {
                    IsViewVisible = false;
                    MainWindow wd = new MainWindow();
                    wd.DataContext = new MainViewModel(wd);
                    Application.Current.MainWindow = wd;
                    wd.ShowDialog();
                }
                return;
            }
        }
        private void SaveToJson(string username, string password)
        {
            // Tạo một đối tượng chứa dữ liệu bạn muốn lưu
            Account account = new Account();
            account.username = username;
            account.password = password;
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
        public LoginViewModel()
        {
            LoginCommand = new ViewModelCommand(ExecuteloginCommand, CanExecuteLoginCommand);
            RecoverPasswordCommnad = new ViewModelCommand(p => ExecuteRecoverPassCommand("",""));
            ReadFromJson();
        }
        private bool authenticate(string tendangnhap, string? matkhau, bool isHashed)
        {
            TAIKHOAN taikhoan = (from m in _db.TAIKHOAN
                                where m.TENDANGNHAP == tendangnhap
                                select m).FirstOrDefault();
            if (taikhoan == null)
            {
                return false;
            }
            if (isHashed)
            {
                if (taikhoan.MATKHAU!= null && SecurePasswordHasher.Verify(matkhau, taikhoan.MATKHAU))
                {
                    SaveToJson(taikhoan.TENDANGNHAP, taikhoan.MATKHAU);
                    return true;
                }
                return false;
            }
            if (taikhoan.MATKHAU != null && taikhoan.MATKHAU.CompareTo(matkhau) == 0)
            {
                SaveToJson(taikhoan.TENDANGNHAP, taikhoan.MATKHAU);
                return true;
            }
            return false;
        }
        private bool CanExecuteLoginCommand(object obj)
        {
            return true;
            /*
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length == 0 || Password == null || Password.Length == 0) 
            {
                validData = false;
            }
            else
            {
                validData = true;
            }
            return validData;
            */
        }
        private void ExecuteloginCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(Username) || Username.Length == 0 || Password == null || Password.Length == 0)
            {
                new MessageBoxCustom("Lỗi", "Vui lòng không để trống password hoặc username", MessageType.Error, MessageButtons.OK).ShowDialog();
                return;
            }
            else
            {

                if (authenticate(Username, Password, true))
                {
                    new MessageBoxCustom("Đăng nhập thành công", "Chào mừng " + Username, MessageType.Success, MessageButtons.OK).ShowDialog();
                }
                else
                {
                    new MessageBoxCustom("Lỗi", "Đăng nhập thất bại", MessageType.Error, MessageButtons.OK).ShowDialog();
                    return;
                }
            }

            IsViewVisible = false;
            MainWindow wd = new MainWindow();
            wd.DataContext = new MainViewModel(wd);
            Application.Current.MainWindow = wd;
            wd.ShowDialog();
        }
        private void ExecuteRecoverPassCommand(string username, string email)
        {
            throw new NotSupportedException();
        }
    }
}
