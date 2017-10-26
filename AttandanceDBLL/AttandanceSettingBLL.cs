using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttandanceDBLL
{
    public class AttandanceSettingBLL
    {
        /***************************/
        ///Add a data into DB
        /***************************/
        public static void Add(Model.AttandanceSetting Model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Attandancesetting values(");
            strSql.Append("'" + Model.MorOnDuty + "',");
            strSql.Append("'" + Model.MorOffDuty + "',");
            strSql.Append("'" + Model.AftOnDuty + "',");
            strSql.Append("'" + Model.AftOffDuty + "'");
            strSql.Append(")");
            DBOperate.DBOperate.ExecuteNoReturn(strSql.ToString());

        }

        public static Model.AttandanceSetting getSetting()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select * from AttandanceSetting ");
            SqlDataReader reader = DBOperate.DBOperate.ExecuteReturn(sb.ToString());
            if (reader == null) return null;
            else
            {
                while (reader.Read())
                {
                    Model.AttandanceSetting temp = new Model.AttandanceSetting(reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString());
                    return temp;
                }
                return null;
            }
        }
    }


}
