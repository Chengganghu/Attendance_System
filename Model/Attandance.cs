using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Attandance
    {
        public Attandance(DateTime attantime,string attantnum,int fingerid,string type,int yesorno,string name,TimeSpan timeSpan)
        { AttantTime = attantime;AttantNum = attantnum;FingerId = fingerid;Type = type;YesOrNo = yesorno;Name = name; TimeSpan = timeSpan; }

        private DateTime? _attantime;
        private string _attantnum;
        private int? _fingerid;
        private string _type;
        private int _yesorno;
        private string _name;
        private TimeSpan _timespan;
        public DateTime? AttantTime { get { return _attantime; } set { _attantime = value; } }
        public string AttantNum { get { return _attantnum; } set { _attantnum = value; } }
        public int? FingerId { get { return _fingerid; }set { _fingerid = value; } }
        public string Type { get { return _type; } set { _type = value; } }
        public int YesOrNo { get { return _yesorno; } set { _yesorno = value; } }
        public string Name { get { return _name; } set { _name = value; } }
        public TimeSpan TimeSpan { get { return this._timespan; } set { this._timespan = value; } }
    }
}
