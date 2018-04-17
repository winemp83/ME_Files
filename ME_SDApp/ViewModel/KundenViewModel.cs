using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME_ViewModel
{
    using ME_ViewModelBase;
    using ME_Model;
    
    public class KundenViewModel : ViewModel<ME_Model.Model.ME_MKunde>
    {
        public KundenViewModel(ME_Model.Model.ME_MKunde model) : base(model)
        {

        }
        public string KundenNummer{get{return Model.KundenNummer;}
            set{if (KundenNummer != null){
                    Model.KundenNummer = value;
                    this.OnPropertyChanged("KundenNummer");}}}
        public string Name{get{ return Model.Name; }
            set{if (Name != null){
                    Model.Name = value;
                    this.OnPropertyChanged("Name");}}}
    }
}
