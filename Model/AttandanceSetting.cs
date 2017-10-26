using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class AttandanceSetting
    {
        public AttandanceSetting(string moronduty,string moroffduty,string aftonfuty,string aftoffduty)
        {
            this.MorOnDuty = moronduty;
            this.MorOffDuty = moroffduty;
            this.AftOnDuty = aftonfuty;
            this.AftOffDuty = aftoffduty;
        }

        private string _moronduty;
        private string _moroffduty;
        private string _aftonduty;
        private string _aftoffduty;

        public string MorOnDuty { get { return this._moronduty; } set { this._moronduty = value; } }
        public string MorOffDuty { get { return this._moroffduty; } set { this._moroffduty = value; } }
        public string AftOnDuty { get { return this._aftonduty; } set { this._aftonduty = value; } }
        public string AftOffDuty { get { return this._aftoffduty; } set { this._aftoffduty = value; } }
    }
}
