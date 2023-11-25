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
        public Victim()
        {
            InitializeComponent();
        }
        public class victim
        {
            public string victim_ID { get; set; }
            public string victim_Name { get; set; }
            public int victim_Age { get; set; }
            public string victim_Sex { get; set; }
            public string victim_Birth { get; set; }
            public string victim_Date { get; set; }
        }
    }
}
