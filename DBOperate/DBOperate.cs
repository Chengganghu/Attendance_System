using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace DBOperate
{
    public class DBOperate
    {
        static string connsql = "Data Source=172.20.14.45;Initial Catalog=Attandance_System;User Id=sa;Password=siat";

        public static bool ExecuteNoReturn(string sql)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = connsql;
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    int res = cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception e)
            {

                return false;
            }

        }

        public static SqlDataReader ExecuteReturn(string sql)
        {
            try
            {
                //using (SqlConnection conn = new SqlConnection())
                //{
                //    conn.ConnectionString = connsql;
                //    conn.Open();
                //    SqlCommand cmd = new SqlCommand(sql, conn);
                //    return cmd.ExecuteReader();
                //}
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = connsql;
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                return cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                
                return null;
            }

        }
    }
}
