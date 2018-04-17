using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME_ViewModel
{
    using ME_ViewModelBase;
    using ME_Model;

    public class NotizenViewModel : ViewModel<ME_Model.Model.ME_MNotiz>
    {
        public NotizenViewModel(ME_Model.Model.ME_MNotiz model) : base(model)
        {

        }
        public string ID
        {
            get { return Model.ID; }
            set
            {
                if (ID != null)
                {
                    Model.ID = value;
                    this.OnPropertyChanged("ID");
                }
            }
        }
        public string SD
        {
            get { return Model.SD; }
            set
            {
                if (SD != null)
                {
                    Model.SD = value;
                    this.OnPropertyChanged("SD");
                }
            }
        }
        public string Kunde
        {
            get { return Model.Kunde; }
            set
            {
                if (Kunde != null)
                {
                    Model.Kunde = value;
                    this.OnPropertyChanged("Kunde");
                }
            }
        }
        public string Date
        {
            get { return Model.Date; }
            set
            {
                if (Date != null)
                {
                    Model.Date = value;
                    this.OnPropertyChanged("Date");
                }
            }
        }
        public string Notiz
        {
            get { return Model.Notiz; }
            set
            {
                if (Notiz != null)
                {
                    Model.Notiz = value;
                    this.OnPropertyChanged("Notiz");
                }
            }
        }
    }
}
