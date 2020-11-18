using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UTS
{
    class Connection
    {
        private string _connectionString = "";

        public Connection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.InitialCatalog = "DB_DATA";
            builder.DataSource = "LAPTOP-H85D22QS\\SQL2019EXPRESS";
            builder.IntegratedSecurity = true;
            this._connectionString = builder.ToString();
        }

        public SqlConnection CreateAndOpenConnection()
        {
            SqlConnection conn = null;
            try
            {
                conn = new SqlConnection(this._connectionString);
                conn.Open();
            }
            catch (Exception)
            {
                throw;
            }
            return conn;
        }
    }
}
