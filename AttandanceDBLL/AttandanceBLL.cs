using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;

namespace AttandanceDBLL
{
    public class AttandanceBLL
    {
        /***************************/
        ///Add a data into DB
        /***************************/
        public static void Add(Attandance Model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Attandance values(");
            strSql.Append("'" + Model.FingerId + "',");
            strSql.Append("'" + Model.AttantTime + "',");
            strSql.Append("'" + Model.AttantNum+ "',");
            strSql.Append("'" + Model.Type + "',");
            strSql.Append(Model.YesOrNo+",'");
            strSql.Append(Model.Name+"',");
            strSql.Append("'" + Model.TimeSpan + "'");
            strSql.Append(")");
            DBOperate.DBOperate.ExecuteNoReturn(strSql.ToString());

        }

        public static List<Attandance> getAllAttant()
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select * from attandance order by attantime");
            SqlDataReader reader = DBOperate.DBOperate.ExecuteReturn(strsql.ToString());
            List<Model.Attandance> mylist = new List<Attandance>();
            if (reader == null) return null;
            else
            {
                while (reader.Read())
                {
                    int i;
                    if (reader[4].ToString().Equals("True")) i = 1;
                    else i = 0;
                    Attandance att = new Attandance(DateTime.Parse(reader[1].ToString()), reader[2].ToString(),
                        Int32.Parse(reader[0].ToString()), reader[3].ToString(),i,reader[5].ToString(),TimeSpan.Parse(reader[6].ToString()));
                    mylist.Add(att);
                }
                return mylist;
            }
        }

        public static List<Model.Attandance> getAttantBytiem(DateTime? BeginTime,DateTime? EndTime)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select * from attandance where (attantime between '");
            strsql.Append(BeginTime);
            strsql.Append("' and '");
            strsql.Append(EndTime);
            strsql.Append("')");
            SqlDataReader reader = DBOperate.DBOperate.ExecuteReturn(strsql.ToString());
            List<Model.Attandance> mylist = new List<Attandance>();
            if (reader == null) return null;
            else
            {
                while (reader.Read())
                {
                    int i;
                    if (reader[4].ToString().Equals("True")) i = 1;
                    else i = 0;
                    Attandance att = new Attandance(DateTime.Parse(reader[1].ToString()), reader[2].ToString(),
                        Int32.Parse(reader[0].ToString()), reader[3].ToString(), i,reader[5].ToString(),TimeSpan.Parse(reader[6].ToString()));
                    mylist.Add(att);
                }
                return mylist;
            }
        }

        public static List<Model.Attandance> getAttantByEmp(string name)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select * from attandance where (name='");
            strsql.Append(name);
            strsql.Append("')");
            SqlDataReader reader = DBOperate.DBOperate.ExecuteReturn(strsql.ToString());
            List<Model.Attandance> mylist = new List<Attandance>();
            if (reader == null) return null;
            else
            {
                while (reader.Read())
                {
                    int i;
                    if (reader[4].ToString().Equals("True")) i = 1;
                    else i = 0;
                    Attandance att = new Attandance(DateTime.Parse(reader[1].ToString()), reader[2].ToString(),
                        Int32.Parse(reader[0].ToString()), reader[3].ToString(), i, reader[5].ToString(),TimeSpan.Parse(reader[6].ToString()));
                    mylist.Add(att);
                }
                return mylist;
            }
        }

        public static List<Model.Attandance> getAttantByType(string type)
        {
            StringBuilder strsql = new StringBuilder();
            strsql.Append("select * from attandance where (type='");
            strsql.Append(type);
            strsql.Append("')");
            SqlDataReader reader = DBOperate.DBOperate.ExecuteReturn(strsql.ToString());
            List<Model.Attandance> mylist = new List<Attandance>();
            if (reader == null) return null;
            else
            {
                while (reader.Read())
                {
                    int i;
                    if (reader[4].ToString().Equals("True")) i = 1;
                    else i = 0;
                    Attandance att = new Attandance(DateTime.Parse(reader[1].ToString()), reader[2].ToString(),
                        Int32.Parse(reader[0].ToString()), reader[3].ToString(), i, reader[5].ToString(),TimeSpan.Parse(reader[6].ToString()));
                    mylist.Add(att);
                }
                return mylist;
            }
        }
    }
}
