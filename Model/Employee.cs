using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Employee:Person
    {
        string _depatementId;
        string _usertype;
        public Employee(string name,int age,string id,DateTime birthday,int fingerId,string depatementId,string usertype) : base(name, age, id, birthday,fingerId)
        {
            this._depatementId = depatementId;
            this._usertype = usertype;
        }

        public string DepatementId { get { return this._depatementId; } set { this._depatementId = value; } }
        public string Usertype { get { return this._usertype; } set { this._usertype = value; } }
    }
}
