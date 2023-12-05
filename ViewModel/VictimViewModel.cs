using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
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
        private IEnumerable<VictimModel> victimList;
        private IVictimRepository victimRepository;
        private string test;
        private List<BENHNHAN> victims;
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        public IEnumerable<VictimModel> VictimList { get => victimList; set
            {
                victimList = value;
                OnPropertyChanged(nameof(VictimList));
            }
            }

        public string Test { get => test; set => test = value; }
        public List<BENHNHAN> Victims { get => victims; set => victims = value; }

        private void Load()
        {
            Victims = _db.BENHNHANs.ToList();
            //System.Windows.MessageBox.Show("Done");
        }

        public VictimViewModel()
        {
            Load();
            /*victimRepository = new VictimRepository();
            LoadAllVictims();*/
        }
        private void LoadAllVictims()
        {
            VictimList = victimRepository.GetByAll();
            Test = "hello world";
            //MessageBox.Show(victimList.Count().ToString());
            /*if (victimList != null) {
                MessageBox.Show(victimList.ToString());
            }*/
            //MessageBox.Show("hello");
        }
    }
}
