using LTTQ_DoAn.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LTTQ_DoAn.View
{
    /// <summary>
    /// Interaction logic for Victim.xaml
    /// </summary>
    public partial class Victim : UserControl
    {
        public ObservableCollection<victim> victims = new ObservableCollection<victim>();
        public Victim()
        {
            InitializeComponent();

            
            /*VictimViewModel victimViewModel = new VictimViewModel();
            this.DataContext = victimViewModel;*/
           /* var list = victimViewModel.VictimList;
            MessageBox.Show(list.ToString()); */


            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            victims.Add(new victim() { ID = "1234", Name = "Vu Thanh Tam", Age = 20, Sex = "male", Birth = "12/6/2003", Date = "21/12/2023" });
            PatientList.ItemsSource = victims;
        }
        public class victim
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Sex { get; set; }
            public string Birth { get; set; }
            public string Date { get; set; }

        }
    }
}
