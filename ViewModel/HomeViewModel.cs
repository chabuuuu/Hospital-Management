using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LTTQ_DoAn.ViewModel
{
    public class HomeViewModel : BaseViewModel
    {
        public string DateTimeFormat = "dd/MM/yyyy";
        public static int divide_number = 5;
        
        private int ysi_count;
        private int victim_count;
        private int field_count;
        private int service_count;
        
        private DateTime chart_startdate = new DateTime(2020, 1, 1);
        private DateTime chart_enddate = DateTime.Now;
        
        //Victim
        private DateTime[] victimDateTime;
        private SeriesCollection victim_series_collections;
        private string[] victimTimeLabels;

        //Doctor and nurse
        private DateTime[] doctorNurseDateTime;
        private SeriesCollection doctorNurse_collections;
        private string[] doctorNurseTimeLabels;

        //Service
        private DateTime[] serviceDateTime;
        private SeriesCollection service_collections;
        private string[] serviceTimeLabels;
        private List<DichVu> listDichVu;

        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();

        public class DichVu
        {
            public string Ten { get; set; }
            public string Doanhthu { get; set; }
        }
        public int Ysi_count
        {
            get => ysi_count; set
            {
                ysi_count = value;
                OnPropertyChanged(nameof(Ysi_count));
            }
        }
        public int Victim_count
        {
            get => victim_count; set
            {
                victim_count = value;
                OnPropertyChanged(nameof(Victim_count));
            }
        }
        public int Field_count { get => field_count; set => field_count = value; }
        public int Service_count
        {
            get => service_count; set
            {
                service_count = value;
                OnPropertyChanged(nameof(Service_count));
            }
        }

        public SeriesCollection Victim_series_collections
        {
            get => victim_series_collections; set
            {
                victim_series_collections = value;
                OnPropertyChanged(nameof(Victim_series_collections));
            }
        }
        public string[] VictimTimeLabels
        {
            get => victimTimeLabels; set
            {
                victimTimeLabels = value;
                OnPropertyChanged(nameof(VictimTimeLabels));
            }
        }
        public Func<double, string> YFormatter { get; set; }

        public Func<double, string> VNDFormatter { get; set; }

        public DateTime Chart_startdate
        {
            get => chart_startdate; set
            {
                chart_startdate = value;
                OnPropertyChanged(nameof(Chart_startdate));
                LoadChart();
            }
        }
        public DateTime Chart_enddate
        {
            get => chart_enddate; set
            {
                chart_enddate = value;
                OnPropertyChanged(nameof(Chart_enddate));
                LoadChart();
            }
        }

        public DateTime[] VictimDateTime
        {
            get => victimDateTime; set
            {
                victimDateTime = value;
                OnPropertyChanged(nameof(VictimDateTime));
            }
        }

        public DateTime[] DoctorNurseDateTime
        {
            get => doctorNurseDateTime; set
            {
                doctorNurseDateTime = value;
                OnPropertyChanged(nameof(DoctorNurseDateTime));
            }
        }
        public SeriesCollection DoctorNurse_collections
        {
            get => doctorNurse_collections; set
            {
                doctorNurse_collections = value;
                OnPropertyChanged(nameof(DoctorNurse_collections));
            }
        }
        public string[] DoctorNurseTimeLabels
        {
            get => doctorNurseTimeLabels; set
            {
                doctorNurseTimeLabels = value;
                OnPropertyChanged(nameof(DoctorNurseTimeLabels));
            }
        }

        public DateTime[] ServiceDateTime
        {
            get => serviceDateTime; set
            {
                serviceDateTime = value;
                OnPropertyChanged(nameof(ServiceDateTime));
            }
        }
        public SeriesCollection Service_collections
        {
            get => service_collections; set
            {
                service_collections = value;
                OnPropertyChanged(nameof(Service_collections));
            }
        }
        public string[] ServiceTimeLabels
        {
            get => serviceTimeLabels; set
            {
                serviceTimeLabels = value;
                OnPropertyChanged(nameof(ServiceTimeLabels));
            }
        }

        public List<DichVu> ListDichVu
        {
            get => listDichVu; set
            {
                listDichVu = value;
                OnPropertyChanged(nameof(ListDichVu));
            }
        }

        public void LoadDoanhThuTheoService(DateTime startdate, DateTime enddate)
        {
            List<DICHVU> default_listService = _db.DICHVU.ToList();
            List<DichVu> listService = new List<DichVu>();
            foreach (var item in default_listService)
            {
                string doanhthu = "0";
                decimal? sumDoanhThu = item.BENHAN
                    .Where(m => m.NGAYKHAM >= startdate && m.NGAYKHAM <= enddate)
                    .Sum(i => i.DICHVU.GIATIEN + i.DONTHUOC
                           .Sum(k => k.CHITIETDONTHUOC
                           .Sum(l => l.THUOC.GIATIEN * (((decimal)((int)(l.SOLUONG * 10000))) / 10000))
                           )
                           );
                if (sumDoanhThu != null)
                {
                    doanhthu = ((decimal)sumDoanhThu).ToString();
                }
                DichVu dichvu = new DichVu() { 
                    Ten = item.TENDICHVU,
                    Doanhthu = doanhthu
                };
                listService.Add(dichvu);
            }
            ListDichVu = listService;
        }

        public void divideTime(int ammount, DateTime startDate, DateTime endDate)
        {
            //int divide = 5;
            //DateTime startDate = new DateTime(2024, 1, 12);
            //DateTime endDate = new DateTime(2024, 3, 30);

            // Tính số ngày cách nhau
            TimeSpan difference = endDate - startDate;

            // Lấy số ngày từ đối tượng TimeSpan
            int numberOfDays = difference.Days;
            DateTime seriesDay = startDate;
            int ammountSpace = numberOfDays / ammount;
            DateTime[] timeLable = new DateTime[ammount + 1];
            for (int i = 0; i <= ammount; i++)
            {
                //Console.WriteLine(seriesDay + "\n");
                timeLable.SetValue(seriesDay, i);
                seriesDay = seriesDay.AddDays(ammountSpace);
            }
            VictimDateTime = timeLable;
            DoctorNurseDateTime = timeLable;
            ServiceDateTime = timeLable;
            string[] timeStringLable = new string[ammount];
            for (int i = 0; i < ammount; i++)
            {
                if (i == ammount - 1)
                {
                    timeStringLable.SetValue(VictimDateTime[i].ToString("yyyy/MM/dd") + "-" + Chart_enddate.ToString("yyyy/MM/dd"), i);
                    break;
                }
               
                //Console.WriteLine(seriesDay + "\n");
                timeStringLable.SetValue(VictimDateTime[i].ToString("yyyy/MM/dd") +"-" + VictimDateTime[i+1].ToString("yyyy/MM/dd"), i);
            }
            VictimTimeLabels = timeStringLable;
            DoctorNurseTimeLabels = timeStringLable;
            ServiceTimeLabels = timeStringLable;
        }
        private int findVictimNumbers(DateTime start_day, DateTime end_date)
        {
            int numbers = (from m in _db.BENHNHAN
                                where m.NGAYNHAPVIEN >= start_day && m.NGAYNHAPVIEN <= end_date
                                select m).Count();
            return numbers;
        }
        private int findDoctorNurseNumbers(DateTime start_day, DateTime end_date)
        {
            int numbers = (from m in _db.YSI
                           where m.NGAYVAOLAM >= start_day && m.NGAYVAOLAM <= end_date
                           select m).Count();
            return numbers;
        }
        private int calServiceNumbers(DateTime start_day, DateTime end_date)
        {
            decimal numbers = 0;
            //Config this because if not, the new that add to now date will not show 
            DateTime configEnd = end_date.AddDays(1);
            decimal? what_numbers = (from m in _db.BENHAN
                           where m.NGAYKHAM >= start_day && m.NGAYKHAM <= configEnd
                           select m)
                           .Sum(i => i.DICHVU.GIATIEN + i.DONTHUOC
                           .Sum(k => k.CHITIETDONTHUOC
                           .Sum(l => l.THUOC.GIATIEN * (((decimal)((int)(l.SOLUONG * 10000))) / 10000))
                           )
                           );
            /* int test = (from m in _db.BENHAN
                                     where m.NGAYKHAM >= start_day && m.NGAYKHAM <= end_date
                                     select m).Count(); */

            if (what_numbers != null)
            {
                numbers = (decimal)what_numbers;
            }
            return Decimal.ToInt32(numbers);
        }
        void Load_Victim_Axis_Y()
        {
            ChartValues<int> chartValues = new ChartValues<int>();
            foreach (var item in VictimTimeLabels)
            {
                DateTime start_date = DateTime.ParseExact(item.Split('-')[0], "yyyy/MM/dd", CultureInfo.InvariantCulture);
                DateTime end_date = DateTime.ParseExact(item.Split('-')[1], "yyyy/MM/dd", CultureInfo.InvariantCulture);
                int count = findVictimNumbers(start_date, end_date);
                chartValues.Add(count);
            }
            Victim_series_collections = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Số bệnh nhân",
                    Values = chartValues
                }
            };


        }
        void Load_DoctorNurse_Axis_Y()
        {
            ChartValues<int> chartValues = new ChartValues<int>();
            foreach (var item in DoctorNurseTimeLabels)
            {
                DateTime start_date = DateTime.ParseExact(item.Split('-')[0], "yyyy/MM/dd", CultureInfo.InvariantCulture);
                DateTime end_date = DateTime.ParseExact(item.Split('-')[1], "yyyy/MM/dd", CultureInfo.InvariantCulture);
                int count = findDoctorNurseNumbers(start_date, end_date);
                chartValues.Add(count);
            }
            DoctorNurse_collections = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Số y sĩ",
                    Values = chartValues
                }
            };


        }

        void Load_Service_Axis_Y()
        {
            ChartValues<int> chartValues = new ChartValues<int>();
            foreach (var item in ServiceTimeLabels)
            {
                DateTime start_date = DateTime.ParseExact(item.Split('-')[0], "yyyy/MM/dd", CultureInfo.InvariantCulture);
                DateTime end_date = DateTime.ParseExact(item.Split('-')[1], "yyyy/MM/dd", CultureInfo.InvariantCulture);
                int count = calServiceNumbers(start_date, end_date);
                chartValues.Add(count);
            }
            Service_collections = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Doanh thu",
                    Values = chartValues
                }
            };
        }
        /*
        void Load_Victim_Chart()
        {
            divideTime(divide_number, Chart_startdate, Chart_enddate);
            Load_Victim_Axis_Y();
        }
        */

        void LoadChart()
        {
            divideTime(divide_number, Chart_startdate, Chart_enddate);
            Ysi_count = findDoctorNurseNumbers(Chart_startdate, Chart_enddate);
            Victim_count = findVictimNumbers(Chart_startdate, Chart_enddate);
            Service_count = calServiceNumbers(Chart_startdate, Chart_enddate);
            Load_Victim_Axis_Y();
            Load_DoctorNurse_Axis_Y();
            Load_Service_Axis_Y();
        }
        public HomeViewModel()
        {
            //Chart_startdate =  new DateTime(2020, 1, 1);
            //Chart_enddate = DateTime.Now;
            /*
            Ysi_count = _db.YSI.Count();
            Victim_count = _db.BENHNHAN.Count();
            Field_count = _db.KHOA.Count();
            Service_count = _db.DICHVU.Count();
            */
            LoadChart();
            LoadDoanhThuTheoService(Chart_startdate, Chart_enddate);
            /*Victim_series_collections = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Số bệnh nhân",
                    Values = new ChartValues<double> { 4, 6, 5, 2 ,4 }
                }
            };
            */
            //VictimTimeLabels = new[] { "Jan", "Feb", "Mar", "Apr", "May" };
            YFormatter = value => value.ToString("F0");
            VNDFormatter = value => value.ToString("C");
        }
    }
}
