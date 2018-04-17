using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME_ViewModel
{
    using ME_ViewModelBase;
    using ME_Model.Model;
    using System.Collections.Specialized;
    using System.Windows.Input;

    public class KundenListViewModel : ViewModel
    {
        public ME_MKunden _K;
        private ObservableCollection<KundenViewModel> _Kunden;
        public ObservableCollection<KundenViewModel> Kunden{
            get { return _Kunden; }
            set {
                if(Kunden != null) {
                    _Kunden = value;
                    this.OnPropertyChanged("Kunden");
                }
            }
        }
        public ME_MKunde Kunde = new ME_MKunde();
        public string ReadFlag { get; set; }
        public string SearchTherm { get; set; }

        public KundenListViewModel(){
            _K = new ME_MKunden();
            _Kunden = new ObservableCollection<KundenViewModel>(_K.Kunden.Select(k => new KundenViewModel(k)));
            _Kunden.CollectionChanged += _Kunden_CollectionChanged;
        }

        private void _Kunden_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            if ( e.Action == NotifyCollectionChangedAction.Add) {
                foreach (KundenViewModel km in e.NewItems)
                    _K.Kunden.Add(km.Model);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (KundenViewModel km in e.OldItems)
                {
                    _K.Kunden.Remove(km.Model);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                _K.Kunden.Clear();
            }
        }
        private ICommand _AddKundenCommand;
        private ICommand _ReadKundenCommand;
        private ICommand _DellKundenCommand;
        private ICommand _EditKundenCommand;

        public ICommand AddKundenCommand{
            get {
                if (_AddKundenCommand == null)
                    _AddKundenCommand = new RelayCommand(p => ExecuteAddKundenCommand());
                return _AddKundenCommand;
            }
        }

        public ICommand DellKundenCommand {
            get {
                if (_DellKundenCommand == null)
                    _DellKundenCommand = new RelayCommand(p => ExecuteDellKundenCommand());
                return _DellKundenCommand;
            }
        }

        public ICommand ReadKundenCommand {
            get {
                if (_ReadKundenCommand == null)
                    _ReadKundenCommand = new RelayCommand(p => ExecuteReadKundenCommand());
                return _ReadKundenCommand;
            }
        }

        public ICommand EdithKundenCommand {
            get {
                if (_EditKundenCommand == null)
                    _EditKundenCommand = new RelayCommand(p => ExecuteEditKundenCommand());
                return _EditKundenCommand;
            }
        }

        private void ExecuteEditKundenCommand() {
            _K.UpdateKunde(Kunde.KundenNummer, Kunde.Name);
            _ReloadList();
        }

        private void ExecuteReadKundenCommand() {
            if (ReadFlag == "KD")
            {
                _K.GetKundeByID(SearchTherm);
                Kunde = _K.Kunden[0];
            }
            else if (ReadFlag == "Name")
            {
                _K.GetKundeByName(SearchTherm);
                Kunde = _K.Kunden[0];
            }
            _K.UpdateList();
            _ReloadList();
        }

        private void ExecuteDellKundenCommand() {
            _K.DeleteKunde(Kunde.KundenNummer);
            _ReloadList();
        }

        private void ExecuteAddKundenCommand(){
            _K.Add(Kunde.KundenNummer, Kunde.Name);
            _ReloadList();
        }

        private void _ReloadList() {
            _Kunden.Clear();
            _Kunden = new ObservableCollection<KundenViewModel>(_K.Kunden.Select(k => new KundenViewModel(k)));
        }
        
    }
    
}
