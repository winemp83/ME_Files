using System;
using System.Collections.Generic;
using System.Linq;

namespace ME_Model.Model
{
    public class ME_MNotiz : IEquatable<ME_MNotiz>
    {
        private int _Id;
        private int _SD;
        private DateTime _Date;
        private int _Kunde;
        private string _Notiz;

        public string ID { get { this._IntToString(this._Id);} set {this._Id = _StringToInt(value); } }
        public string SD { get { _IntToString(this._SD);} set { this._SD = _StringToInt(value); }}
        public string Notiz { get { this._Notiz;} set { this._Notiz = value; }}
        public string Date { get { _Date.ToShortDateString();} set {this._StringToDate(value); } }
        public string Kunde { get { _IntToString(this._Kunde);} set { this._Kunde = _StringToInt(value); }}


        private int _StringToInt(string value) {return Convert.ToInt32(value);}
        private string _IntToString(int value) {return Convert.ToString(value);}
        private string _StringToDate(string d){
            try { this._Date = DateTime.Parse(d); return d; } catch (Exception) { this._Date = DateTime.Now; return DateTime.Now.ToShortDateString(); }
        }

        public ME_MNotiz(string notiz){
            this._Id = 0;
            this._SD = 0;
            this._Date = DateTime.Now;
            this._Kunde = 0;
            this._Notiz = notiz;
        }

        public ME_MNotiz(string id="0", string sd = "0", string kunde = "0",string datum = "01.01.2018",  string notiz = ""){
            this.ID = id;
            this.SD = sd;
            this.Kunde = kunde;
            this.Date = datum;
            this.Notiz = notiz;
        }



        public override string ToString() { return string.Format("[ID: {0} , SD = {1} , Datum = {2}, Kunde = {3}, Notiz : {4} ]", this.ID, this.SD, this.Date, this.Kunde, this.Notiz); }

        public override bool Equals(object obj)
        {
            return Equals(obj as ME_MNotiz);
        }

        public bool Equals(ME_MNotiz other)
        {
            return other != null &&
                   ID == other.ID &&
                   SD == other.SD &&
                   Date == other.Date &&
                   Kunde == other.Kunde &&
                   Notiz == other.Notiz;
        }

        public override int GetHashCode()
        {
            var hashCode = 1826351141;
            hashCode = hashCode * -1521134295 + _Id.GetHashCode();
            hashCode = hashCode * -1521134295 + _SD.GetHashCode();
            hashCode = hashCode * -1521134295 + _Date.GetHashCode();
            hashCode = hashCode * -1521134295 + _Kunde.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_Notiz);
            return hashCode;
        }
    }

    public class ME_MNotizen : IEquatable<ME_MNotizen>
    {
        private Funktionen.ME_XmLDBNotizen db = new Funktionen.ME_XmLDBNotizen();
        public IList<ME_MNotiz> Notizen;

        public ME_MNotizen()
        {
            Notizen = new List<ME_MNotiz>();
            UpdateList();
        }

        public void UpdateList()
        {
            Notizen.Clear();
            Notizen = db.Read();
        }

        public void AddNotiz(string id = "0", string SD = "00000", string datum = "01.01.2018", string kunde = "00000", string text = "")
        {
            db.Write(new ME_MNotiz(id , SD, kunde, datum, text));
            UpdateList();
        }

        public void UpdateNotiz(string id = "0", string SD = "00000", string datum = "01.01.2018", string kunde = "00000", string text = "")
        {
            db.Update(new ME_MNotiz(id, SD, kunde, datum, text));
            UpdateList();
        }

        public void DeleteNotiz(string id)
        {
            db.Delete(new ME_MNotiz(id));
            UpdateList();
        }

        public ME_MNotiz GetNotizByID(string id)
        {
            IList<ME_MNotiz> _tmp = new List<ME_MNotiz>();
            _tmp = db.Read("ID", id);
            foreach (ME_MNotiz k in _tmp)
                return k;
            return new ME_MNotiz(id: "0");
        }

        public void GetNotizByKunde(string value)
        {
            Notizen = db.Read("KD", value);
        }

        public void GetNotizBySD(string value)
        {
            Notizen = db.Read("SD", value);
        }

        public string Count()
        {
            UpdateList();
            return Notizen.Count() + "";
        }

        public void Debug()
        {
            foreach (ME_MNotiz _tmp in Notizen)
                System.Diagnostics.Debug.WriteLine(_tmp.ToString());
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ME_MNotizen);
        }

        public bool Equals(ME_MNotizen other)
        {
            return other != null &&
                   EqualityComparer<IList<ME_MNotiz>>.Default.Equals(Notizen, other.Notizen);
        }

        public override int GetHashCode()
        {
            return 564816536 + EqualityComparer<IList<ME_MNotiz>>.Default.GetHashCode(Notizen);
        }
    }

    
}