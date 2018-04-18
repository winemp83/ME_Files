using System;
using System.Windows;
using ViewModel;

namespace StartApplication
{
    /// <summary>
    /// Interaktionslogik für MainWindow1.xaml
    /// </summary>
    public partial class MainWindow1 : Window
    {
        public MainWindow1()
        {
            InitializeComponent();
        }

        void OnAddClick(object sender, EventArgs e)
        {
            //For each dialog we use the same instance of ViewModel
            var customerDialogBox = new CustomerEditorView(((MainViewModel)this.DataContext).CustomerEditorViewModel);

            //No need to check DialogResult - it is respnsibility of ViewModel to interpret the result of commands
            //View only displays the window
            customerDialogBox.ShowDialog();
        }
        void OnSaveClick(object sender, EventArgs e) {
            
        }
        void OnLoadClick(object sender, EventArgs e) { 
            
        }
    }
}
