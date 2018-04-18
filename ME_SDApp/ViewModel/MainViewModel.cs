using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace ViewModel
{
    public class MainViewModel
    {
        private ME_ViewModel.Funktionen.ME_XmLDBKunden db;
        public ObservableCollection<CustomerItemViewModel> Customers { get; set; }

        public CustomerEditorViewModel CustomerEditorViewModel { get; set; }

        public MainViewModel()
        {
            this.Customers = new ObservableCollection<CustomerItemViewModel>();
            this.db = new ME_ViewModel.Funktionen.ME_XmLDBKunden();
            //ViewModel is aware of existence of another ViewModel for editing customer item
            //but it does not need to be a "dialog" - it can be tabPanel, collapsable panel etc
            //the important thing here is that from this ViewModel it is possible to "Save" the data
            this.CustomerEditorViewModel = new CustomerEditorViewModel();
            this.CustomerEditorViewModel.Save += CustomerViewModelOnSave;
            this.CustomerEditorViewModel.Load += CustomerOnLoadClick;
            foreach (CustomerItemViewModel m in db.Read())
            {
                this.Customers.Add(m);
            }
        }

        private void CustomerOnLoadClick(object sender, EventArgs e)
        {
            this.Customers.Clear();
            foreach (CustomerItemViewModel m in db.Read())
                Customers.Add(m);
        }

        private void CustomerViewModelOnSave(object sender, EventArgs eventArgs)
        {
            db.Write(new CustomerItemViewModel(this.CustomerEditorViewModel.KundenName, this.CustomerEditorViewModel.KundenNummer));
            this.Customers.Add(new CustomerItemViewModel(this.CustomerEditorViewModel.KundenName, this.CustomerEditorViewModel.KundenNummer));
            this.CustomerEditorViewModel.Reset();
        }



    }
}
