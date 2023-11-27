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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LTTQ_DoAn.View
{
    /// <summary>
    /// Interaction logic for field.xaml
    /// </summary>
    public partial class field : UserControl
    {
        List<string> fields = new List<string>();
        public field()
        {
            InitializeComponent();
            fields = new List<string>() { "khoa tim mạch", "khoa da liễu", "khoa ngoại", "khoa tiêu hóa", "khoa tiết niệu", 
                "khoa nội thần kinh", "khoa ngoại thần kinh", "nhãn khoa", "khoa nhi", "khoa tai-mũi-họng", "khoa nam", "phụ khoa",
                "khoa nội thần kinh", "khoa ngoại thần kinh", "nhãn khoa", "khoa nhi", "khoa tai-mũi-họng", "khoa nam", "phụ khoa"
                ,"khoa tim mạch", "khoa da liễu", "khoa ngoại", "khoa tiêu hóa", "khoa tiết niệu",
                "khoa nội thần kinh", "khoa ngoại thần kinh", "nhãn khoa", "khoa nhi", "khoa tai-mũi-họng", "khoa nam", "phụ khoa",
                "khoa nội thần kinh", "khoa ngoại thần kinh", "nhãn khoa", "khoa nhi", "khoa tai-mũi-họng", "khoa nam", "phụ khoa"};
            FieldList.ItemsSource = fields;
        }
        
    }
}
