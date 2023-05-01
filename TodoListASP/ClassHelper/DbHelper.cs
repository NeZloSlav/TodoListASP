using System.Data.SqlClient;

namespace TodoListASP.ClassHelper
{
    public class DbHelper
    {
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TodoListASPDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;");
        }
    }
}
