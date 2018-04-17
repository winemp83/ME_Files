using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ME_ViewModel
{
    using ME_ViewModelBase;
    using ME_Model.Model;
    using System.Collections.Specialized;
    using System.Windows.Input;
    using System.Collections.ObjectModel;

    public class NotizenListViewModel : ViewModel
    {
        public ME_MNotizen _N;
        private ObservableCollection<NotizenViewModel> _Notizen;
        public ObservableCollection<NotizenViewModel> Notizen
        {
            get { return _Notizen; }
            set
            {
                if (Notizen != null)
                {
                    _Notizen = value;
                    this.OnPropertyChanged("Notizen");
                }
            }
        }
        public ME_MNotiz Notiz = new ME_MNotiz();
        public string ReadFlag { get; set; }
        public string SearchTherm { get; set; }

        public NotizenListViewModel()
        {
            _N = new ME_MNotizen();
            _Notizen = new ObservableCollection<NotizenViewModel>(_N.Notizen.Select(k => new NotizenViewModel(k)));
            _Notizen.CollectionChanged += _Notizen_CollectionChanged;
        }

        private void _Notizen_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (NotizenViewModel km in e.NewItems)
                    _N.Notizen.Add(km.Model);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (NotizenViewModel km in e.OldItems)
                {
                    _N.Notizen.Remove(km.Model);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                _N.Notizen.Clear();
            }
        }
        private ICommand _AddNotizenCommand;
        private ICommand _ReadNotizenCommand;
        private ICommand _DellNotizenCommand;
        private ICommand _EditNotizenCommand;

        public ICommand AddNotizenCommand
        {
            get
            {
                if (_AddNotizenCommand == null)
                    _AddNotizenCommand = new RelayCommand(p => ExecuteAddNotizenCommand());
                return _AddNotizenCommand;
            }
        }

        public ICommand DellNotizenCommand
        {
            get
            {
                if (_DellNotizenCommand == null)
                    _DellNotizenCommand = new RelayCommand(p => ExecuteDellNotizenCommand());
                return _DellNotizenCommand;
            }
        }

        public ICommand ReadNotizenCommand
        {
            get
            {
                if (_ReadNotizenCommand == null)
                    _ReadNotizenCommand = new RelayCommand(p => ExecuteReadNotizenCommand());
                return _ReadNotizenCommand;
            }
        }

        public ICommand EdithNotizenCommand
        {
            get
            {
                if (_EditNotizenCommand == null)
                    _EditNotizenCommand = new RelayCommand(p => ExecuteEditNotizenCommand());
                return _EditNotizenCommand;
            }
        }

        private void ExecuteEditNotizenCommand()
        {
            _N.UpdateNotiz(Notiz.ID, Notiz.SD, Notiz.Date, Notiz.Kunde, Notiz.Notiz);
            _ReloadList();
        }

        private void ExecuteReadNotizenCommand()
        {
            if (ReadFlag == "ID")
            {
                _N.GetNotizByID(SearchTherm);
                Notiz = _N.Notizen[0];
            }
            else if (ReadFlag == "SD")
            {
                _N.GetNotizBySD(SearchTherm);
                Notiz = _N.Notizen[0];
            }
            _N.UpdateList();
            _ReloadList();
        }

        private void ExecuteDellNotizenCommand()
        {
            _N.DeleteNotiz(Notiz.ID);
            _ReloadList();
        }

        private void ExecuteAddNotizenCommand()
        {
            _N.AddNotiz(Notiz.ID, Notiz.SD, Notiz.Date, Notiz.Kunde, Notiz.Notiz);
            _ReloadList();
        }

        private void _ReloadList()
        {
            _Notizen.Clear();
            _Notizen = new ObservableCollection<NotizenViewModel>(_N.Notizen.Select(k => new NotizenViewModel(k)));
        }
    }
}