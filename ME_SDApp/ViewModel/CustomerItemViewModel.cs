using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class CustomerItemViewModel
    {
        private string _Kundenname;
        private string _Kundennummer;

        public CustomerItemViewModel(string _KundenName, string _KundenNummer)
        {
            this._Kundenname = _KundenName;
            this._Kundennummer = _KundenNummer;
        }

        public override string ToString()
        {
            return string.Format("Kunde : {0}, ID :  {1}", this._Kundenname, this._Kundennummer);
        }

        public string KundenName { get { return _Kundenname; } set { _Kundenname = value; } }
        public string KundenNummer { get { return _Kundennummer; } set { _Kundennummer = value; } }
    }
}
