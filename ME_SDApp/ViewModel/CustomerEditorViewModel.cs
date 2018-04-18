using ME_ViewModelBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class CustomerEditorViewModel
    {
        public string KundenName { get; set; }

        public string KundenNummer { get; set; }

        public event EventHandler Save;
        public event EventHandler Load;
        public event EventHandler Add;
        
        public RelayCommand SaveCommand { get; set; }
        public RelayCommand LoadCommand { get; set;}

        public CustomerEditorViewModel()
        {
            this.SaveCommand = new RelayCommand(x => this.Save(this, new EventArgs()));
            this.LoadCommand = new RelayCommand(x => this.Load(this, new EventArgs()));
        }

        public void Reset()
        {
            this.KundenName = "";
            this.KundenNummer = "";
        }
    }
}
