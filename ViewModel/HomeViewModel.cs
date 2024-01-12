using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTTQ_DoAn.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        private int ysi_count;
        private int victim_count;
        private int field_count;
        private int service_count;
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();


        public int Ysi_count { get => ysi_count; set => ysi_count = value; }
        public int Victim_count { get => victim_count; set => victim_count = value; }
        public int Field_count { get => field_count; set => field_count = value; }
        public int Service_count { get => service_count; set => service_count = value; }
        public HomeViewModel()
        {
            Ysi_count = _db.YSI.Count();
            Victim_count = _db.BENHNHAN.Count();
            Field_count = _db.KHOA.Count();
            Service_count = _db.DICHVU.Count();
        }
    }
}
