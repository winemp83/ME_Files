using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class MainViewModel
    {
        public ObservableCollection<CustomerItemViewModel> Customers { get; set; }

        public CustomerEditorViewModel CustomerEditorViewModel { get; set; }

        public MainViewModel()
        {
            this.Customers = new ObservableCollection<CustomerItemViewModel>();

            //ViewModel is aware of existence of another ViewModel for editing customer item
            //but it does not need to be a "dialog" - it can be tabPanel, collapsable panel etc
            //the important thing here is that from this ViewModel it is possible to "Save" the data
            this.CustomerEditorViewModel = new CustomerEditorViewModel();
            this.CustomerEditorViewModel.Save += CustomerViewModelOnSave;
            this.CustomerEditorViewModel.Load += CustomerViewModelOnLoad;
        }

        private void CustomerViewModelOnSave(object sender, EventArgs eventArgs)
        {
            ME_ViewModel.Funktionen.ME_XmLDBKunden db = new ME_ViewModel.Funktionen.ME_XmLDBKunden();
            db.Write(new CustomerItemViewModel(this.CustomerEditorViewModel.KundenName, this.CustomerEditorViewModel.KundenNummer));
            this.Customers.Add(new CustomerItemViewModel(this.CustomerEditorViewModel.KundenName, this.CustomerEditorViewModel.KundenNummer));
            this.CustomerEditorViewModel.Reset();
        }

        private void CustomerViewModelOnLoad(object sender, EventArgs eventArgs) {

        }

    }
}
