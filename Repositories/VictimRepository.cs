using LTTQ_DoAn.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Data.SqlClient;
using System.Windows;
using System.Collections.ObjectModel;

namespace LTTQ_DoAn.Repositories
{
    public class VictimRepository : RepositoryBase, IVictimRepository
    {
        public void Add(VictimModel victimModel)
        {
            throw new NotImplementedException();
        }

        public void Edit(UserModel victimModel)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VictimModel> GetByAll()
        {
            //VictimModel victim = null;
            List<VictimModel> victimList = new List<VictimModel>(); // Khởi tạo danh sách trước khi sử dụng

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select * from BENHNHAN";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        VictimModel victim = new VictimModel()
                        {
                            MABENHNHAN = reader["MABENHNHAN"].ToString(),
                            MAPHONG = reader["MAPHONG"].ToString(),
                            HOTEN = reader["HOTEN"].ToString(),
                            GIOITINH = reader["GIOITINH"].ToString(),
                            NGAYSINH = reader["NGAYSINH"].ToString(),
                            DIACHI = reader["DIACHI"].ToString(),
                            MABHYT = reader["MABHYT"].ToString(),
                            NGAYNHAPVIEN = reader["NGAYNHAPVIEN"].ToString()
                        };
                        //MessageBox.Show(reader["MABENHNHAN"].ToString());
                        //MessageBox.Show(victim.MABENHNHAN.ToString());
                        victimList.Add(victim);
                    }
                }




                //MessageBox.Show("hello");
                /* using (var reader = command.ExecuteReader())
                 {
                     if (reader.Read())
                     {
                         foreach (SqlDataReader item in reader)
                         {
                             victim = new VictimModel()
                             {
                                 MABENHNHAN = item[0].ToString(),
                                 MAPHONG = item[1].ToString(),
                                 HOTEN = item[2].ToString(),
                                 GIOITINH = item[3].ToString(),
                                 NGAYSINH = item[4].ToString(),
                                 DIACHI = item[5].ToString(),
                                 MABHYT = item[6].ToString(),
                                 NGAYNHAPVIEN = item[7].ToString()
                             };
                             victimList.Append(victim);
                         }
                         //MessageBox.Show(victimList.ToString());

                     }


                 } */
            }
            //MessageBox.Show(victimList.ToString());
            return victimList;
        }

        public VictimModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public VictimModel GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
