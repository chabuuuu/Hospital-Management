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
        private DateTime victim_startdate = new DateTime(2020, 1, 1);
        private DateTime victim_enddate = DateTime.Now;
        private DateTime[] victimDateTime;
        private SeriesCollection victim_series_collections;
        QUANLYBENHVIENEntities _db = new QUANLYBENHVIENEntities();
        private string[] victimTimeLabels;

        public int Ysi_count { get => ysi_count; set => ysi_count = value; }
        public int Victim_count
        {
            get => victim_count; set
            {
                victim_count = value;
                OnPropertyChanged(nameof(Victim_count));
            }
        }
        public int Field_count { get => field_count; set => field_count = value; }
        public int Service_count { get => service_count; set => service_count = value; }
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
        public DateTime Victim_startdate
        {
            get => victim_startdate; set
            {
                victim_startdate = value;
                OnPropertyChanged(nameof(Victim_startdate));
                Load_Victim_Chart();
            }
        }
        public DateTime Victim_enddate
        {
            get => victim_enddate; set
            {
                victim_enddate = value;
                OnPropertyChanged(nameof(Victim_enddate));
                Load_Victim_Chart();
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
            string[] timeStringLable = new string[ammount];
            for (int i = 0; i < ammount; i++)
            {
                if (i == ammount - 1)
                {
                    timeStringLable.SetValue(VictimDateTime[i].ToString("yyyy/MM/dd") + "-" + Victim_enddate.ToString("yyyy/MM/dd"), i);
                    break;
                }
               
                //Console.WriteLine(seriesDay + "\n");
                timeStringLable.SetValue(VictimDateTime[i].ToString("yyyy/MM/dd") +"-" + VictimDateTime[i+1].ToString("yyyy/MM/dd"), i);
            }
            VictimTimeLabels = timeStringLable;
        }
        private int findVictimNumbers(DateTime start_day, DateTime end_date)
        {
            int numbers = (from m in _db.BENHNHAN
                                where m.NGAYNHAPVIEN >= start_day && m.NGAYNHAPVIEN <= end_date
                                select m).Count();
            return numbers;
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

        void Load_Victim_Chart()
        {
            divideTime(divide_number, Victim_startdate, Victim_enddate);
            Load_Victim_Axis_Y();
        }
        public HomeViewModel()
        {
            //Victim_startdate =  new DateTime(2020, 1, 1);
            //Victim_enddate = DateTime.Now;
            Ysi_count = _db.YSI.Count();
            Victim_count = _db.BENHNHAN.Count();
            Field_count = _db.KHOA.Count();
            Service_count = _db.DICHVU.Count();
            Load_Victim_Chart();
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
        }
    }
}
