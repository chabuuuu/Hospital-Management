using LTTQ_DoAn.Model;
using LTTQ_DoAn.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LTTQ_DoAn.ViewModel
{
    public class VictimViewModel: BaseViewModel
    {
        private IEnumerable<VictimModel> victimList;
        private IVictimRepository victimRepository;
        private string test;

        public IEnumerable<VictimModel> VictimList { get => victimList; set
            {
                victimList = value;
                OnPropertyChanged(nameof(VictimList));
            }
            }

        public string Test { get => test; set => test = value; }

        public VictimViewModel()
        {
            victimRepository = new VictimRepository();
            LoadAllVictims();
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
