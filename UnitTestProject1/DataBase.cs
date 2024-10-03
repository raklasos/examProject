using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace UnitTestProject1
{
    internal class DataBase
    {
        SqlConnection sqlConnection = new SqlConnection(@"Data Source=ADCLG1; Initial Catalog=Z_429192_Grezin;Integrated Security=True");

        public void openConnection()
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
                sqlConnection.Open();
        }

        public void closeConnection()
        {
            if (sqlConnection.State != System.Data.ConnectionState.Open)
                sqlConnection.Close();
        }

        public SqlConnection GetConnection() { return sqlConnection; }


    }
}
