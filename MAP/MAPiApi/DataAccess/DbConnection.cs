using System.Data.SqlClient;

namespace MAPiApi.DataAccess
{
    public class DbConnection
    {
        private SqlConnection con = null;

        public DbConnection()
        {
            con = new SqlConnection(@"Data Source=localhost\MSSQLEXPRESS2014;Initial Catalog=dmaj0916_197331;Integrated Security=True");
            //con = new SqlConnection("Server=kraka.ucn.dk; database=dmaj0916_197331; UID=dmaj0916_197331; password=Password1!; MultipleActiveResultSets=True");
            con.Open();
        }

        public SqlConnection GetConnection()
        {
            return con;
        }
    }
}
