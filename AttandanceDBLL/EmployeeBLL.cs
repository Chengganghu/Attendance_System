using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Collections;
using System.Data.SqlClient;

namespace AttandanceDBLL
{
    public class EmployeeBLL
    {
        /***************************/
        ///Add a data into DB
        /***************************/
        public static void Add(Employee Model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Employee values(");
            strSql.Append("'" + Model.Id+"',");
            strSql.Append("'" + Model.Name + "',");
            strSql.Append("'" + Model.Birthday + "',");
            strSql.Append("'" + Model.Age + "',");
            strSql.Append("'" + Model.FingerId + "',");
            strSql.Append("'" + Model.DepatementId + "',");
            strSql.Append("'" + Model.Usertype + "'"); 
            strSql.Append(")");
            DBOperate.DBOperate.ExecuteNoReturn(strSql.ToString());

        }
        /***************************/
        ///Delete a data from DB
        /***************************/
        public static void Delete(string EmpId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete Employee ");
            strSql.Append(" where Id='" + EmpId + "'");
            DBOperate.DBOperate.ExecuteNoReturn(strSql.ToString());
        }

        /**************************/
        ///
        ///return a instance of Employee from datebase by knowing id
        ///
        /**************************/
        public static Model.Employee getEmp(string Id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from Employee ");
            sb.Append("where id='" + Id +"'");
            SqlDataReader reader = DBOperate.DBOperate.ExecuteReturn(sb.ToString());
            if (reader == null) return null;
            else
            {
                while (reader.Read())
                {
                    Employee temp = new Employee(reader[1].ToString(), Int32.Parse(reader[3].ToString()),
                    reader[0].ToString(), DateTime.Parse(reader[2].ToString()),
                    Int32.Parse(reader[4].ToString()), reader[5].ToString(), reader[6].ToString());
                    return temp;
                }
                return null;
            }
        }

        /**************************/
        ///
        ///return a instance of Employee from datebase by knowing FingerId
        ///
        /**************************/
        public static Model.Employee getEmpByFinger(string fingerId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from Employee ");
            sb.Append("where FingerId='" + fingerId + "'");
            SqlDataReader reader = DBOperate.DBOperate.ExecuteReturn(sb.ToString());
            if (reader == null) return null;
            else
            {
                while (reader.Read())
                {
                    Employee temp = new Employee(reader[1].ToString(), Int32.Parse(reader[3].ToString()),
                    reader[0].ToString(), DateTime.Parse(reader[2].ToString()),
                    Int32.Parse(reader[4].ToString()), reader[5].ToString(), reader[6].ToString());
                    return temp;
                }
                return null;
            }
        }
        /**************************/
        ///
        ///return all the instances of Employee from datebase
        ///
        /**************************/
        public static List<Model.Employee> getAllEmp()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from Employee ");
            SqlDataReader reader = DBOperate.DBOperate.ExecuteReturn(sb.ToString());
            List<Model.Employee> myList = new List<Employee>();
            if (reader == null) return null;
            else
            {
                while (reader.Read())
                {
                    Employee temp = new Employee(reader[1].ToString(), Int32.Parse(reader[3].ToString()),
                    reader[0].ToString(), DateTime.Parse(reader[2].ToString()),
                    Int32.Parse(reader[4].ToString()), reader[5].ToString(), reader[6].ToString());
                    myList.Add(temp);
                }
                return myList;
            }
        }
    }
}
