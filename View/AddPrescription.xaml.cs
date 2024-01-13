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
    /// Interaction logic for Prescription.xaml
    /// </summary>
    public partial class AddPrescription : Window
    {
        public AddPrescription()
        {
            InitializeComponent();
        }

        private void Prescription_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                e.Handled = true;
                Prescription.Text += Environment.NewLine;
                Prescription.CaretIndex = Prescription.Text.Length;
            }
        }
    }
}
