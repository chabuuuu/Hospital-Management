using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace LTTQ_DoAn.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;
        public RepositoryBase()
        {
            _connectionString = "Server=(local); Database=QUANLYBENHVIEN; Integrated Security=true";
        }

        // kết nối câu lệnh kết nối sql
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}