using LTTQ_DoAn.ViewModel;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace LTTQ_DoAn.View
{
    /// <summary>
    /// Interaction logic for AddVictim.xaml
    /// </summary>
    public partial class AddVictim : Window
    {
        public AddVictim()
        {
            InitializeComponent();
        }
        public AddVictim(int id)
        {
            InitializeComponent();
            this.DataContext = new AddVictimViewModel(id);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
