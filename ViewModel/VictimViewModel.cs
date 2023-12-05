using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace LTTQ_DoAn.ViewModel
{
    public class VictimViewModel: BaseViewModel
    {
        private List<BENHNHAN> victims;
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

        public List<BENHNHAN> Victims { get => victims; set => victims = value; }

        private void Load()
        {
            Victims = _db.BENHNHANs.ToList();
            //System.Windows.MessageBox.Show("Done");
        }

        public VictimViewModel()
        {
            Load();
        }
    }
}
