using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Person
    {
        private string _name;
        private int? _age;
        private string _id;
        private DateTime? _birthday;
        private int? _fingerId;

        public Person(string name,int age,string id,DateTime birthday,int fingerId)
        {
            this._name = name;
            this._age = age;
            this._id = id;
            this._birthday = birthday;
            this._fingerId = fingerId;
        }

        public string Name { get { return this._name; } set { this._name = value; } }
        public int? Age { get { return this._age; } set { this._age = value; } }
        public string Id { get { return this._id; } set { this._id = value; } }
        public DateTime? Birthday { get { return this._birthday; } set { this._birthday = value; } }
        public int? FingerId { get { return this._fingerId; } set { this._fingerId = value; } }
    }
}
