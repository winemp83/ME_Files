using System;
using ViewModel;

namespace StartApplication
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class CustomerEditorView
    {
        public CustomerEditorView()
        {
            InitializeComponent();
        }

        public CustomerEditorView(CustomerEditorViewModel context) : this()
        {
            this.DataContext = context;
            context.Save += Save;
        }

        void Save(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
