using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;
using System.Data.SqlClient;
namespace AttandanceDBLL
{
    public class LoginBLL
    {
        /***************************/
        ///Add a data into DB
        /***************************/
        public static void Add(Login Model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Login values(");
            strSql.Append("'" + Model.Username + "',");
            strSql.Append("'" + Model.Password + "',");
            strSql.Append("'" + Model.Id+"'");
            strSql.Append(")");
            bool result = DBOperate.DBOperate.ExecuteNoReturn(strSql.ToString());

        }

        /**************************/
        ///
        ///return a instance of Login from datebase
        ///
        /**************************/
        public static System.Collections.ArrayList getLogin(string Username)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from Login ");
            sb.Append("where username='" + Username+"'");
            SqlDataReader reader = DBOperate.DBOperate.ExecuteReturn(sb.ToString());
            if (reader == null) return null;
            else
            {
                System.Collections.ArrayList list = new System.Collections.ArrayList();
                while(reader.Read())
                {
                    Login temp = new Login(reader[0].ToString(), reader[1].ToString(), reader[2].ToString());
                    list.Add(temp);
                }
                reader.Close();
                return list;
            }
        }
        /***************************/
        ///Delete a data from DB
        /***************************/
        public static void Delete(string EmpId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete Login ");
            strSql.Append(" where Id='" + EmpId + "'");
            DBOperate.DBOperate.ExecuteNoReturn(strSql.ToString());
        }
    }
}
