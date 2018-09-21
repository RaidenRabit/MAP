using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MAPeApi.DataAccess
{
    public class DbConnection
    {
        private SqlConnection con = null;

        public DbConnection()
        {
            con = new SqlConnection("Server=kraka.ucn.dk; database=dmaj0916_197331; UID=dmaj0916_197331; password=Password1!; MultipleActiveResultSets=True");
            con.Open();
        }

        public SqlConnection GetConnection()
        {
            return con;
        }
        
    }
}
