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
    /// Interaction logic for appointment.xaml
    /// </summary>
    public partial class appointment : UserControl
    {
        public appointment()
        {
            InitializeComponent();
            ObservableCollection<Appointment> Appointments = new ObservableCollection<Appointment>();
            Appointments.Add(new Appointment() { ID = "123", Name = "asdads", Age = 12, Service = "asdasdsa", Date = "12/4/4" });
            AppointmentList.ItemsSource = Appointments;
        }
        public class Appointment
        {
            public string ID { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public string Service { get; set; }
            public string Date { get; set; }

        }
    }
}
