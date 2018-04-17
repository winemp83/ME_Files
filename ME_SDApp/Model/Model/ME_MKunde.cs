using System;
using System.Collections.Generic;
using System.Linq;

namespace ME_Model.Model
{
    public class ME_MKunde : IEquatable<ME_MKunde>
    {
        private int _KundenNummer;
        private string _Name;

        public string KundenNummer { get { return _IntToString(this._KundenNummer); } set { this._KundenNummer = _StringToInt(value); } }
        public string Name { get { return _Name; } set { this._Name = value; } }

        public ME_MKunde(){
            this._KundenNummer = 0;
            this._Name = "";
        }

        public ME_MKunde(string kd = "0", string name = "") {
            this._KundenNummer = Convert.ToInt32(kd);
            this._Name = name;
        }

        private int _StringToInt(string value) => Convert.ToInt32(value);
        private string _IntToString(int value) => Convert.ToString(value);
        public override string ToString() => string.Format("KD: {0}, Name:{1}", this._KundenNummer, this._Name);

        public override bool Equals(object obj)
        {
            return Equals(obj as ME_MKunde);
        }

        public bool Equals(ME_MKunde other)
        {
            return other != null &&
                   _KundenNummer == other._KundenNummer &&
                   _Name == other._Name;
        }

        public override int GetHashCode()
        {
            var hashCode = -1437779364;
            hashCode = hashCode * -1521134295 + _KundenNummer.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_Name);
            return hashCode;
        }
    }

    public class ME_MKunden : IEquatable<ME_MKunden>
    {
        private Funktionen.ME_XmLDBKunden db = new Funktionen.ME_XmLDBKunden();
        public IList<ME_MKunde> Kunden;

        public ME_MKunden(){
            Kunden = new List<ME_MKunde>();
            this.UpdateList();
        }

        public void UpdateList(){
            Kunden.Clear();
            Kunden = db.Read();
        }

        public void Add(string KundenNummer = "00000", string Name = "") {
            db.Write(new ME_MKunde(KundenNummer, Name));
            UpdateList();
        }

        public void UpdateKunde(string KundenNummer, string Name) {
            db.Update(new ME_MKunde(KundenNummer, Name));
            UpdateList();
        }

        public void DeleteKunde(string KundenNummer)
        {
            db.Delete(new ME_MKunde(KundenNummer));
            UpdateList();
        }

        public ME_MKunde GetKundeByID(string KD){
            IList<ME_MKunde> _tmp = new List<ME_MKunde>();
            _tmp = db.Read("KD", KD);
            foreach (ME_MKunde k in _tmp)
                return k;
            return new ME_MKunde(name: "null");
        }

        public ME_MKunde GetKundeByName(string value){
            IList<ME_MKunde> _tmp = new List<ME_MKunde>();
            _tmp = db.Read("Name", value);
            foreach (ME_MKunde k in _tmp)
                return k;
            return new ME_MKunde(name: "null");
        }

        public string Count() {
            UpdateList();
            return Kunden.Count()+"";
        }

        public void Debug(){
            foreach (ME_MKunde _tmp in Kunden)
                System.Diagnostics.Debug.WriteLine(_tmp.ToString());
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ME_MKunden);
        }

        public bool Equals(ME_MKunden other)
        {
            return other != null &&
                   EqualityComparer<IList<ME_MKunde>>.Default.Equals(Kunden, other.Kunden);
        }

        public override int GetHashCode()
        {
            return 564816536 + EqualityComparer<IList<ME_MKunde>>.Default.GetHashCode(Kunden);
        }
    }
}