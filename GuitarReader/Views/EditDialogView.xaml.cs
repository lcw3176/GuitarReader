using GuitarReader.ViewModels;
using System.Windows;

namespace GuitarReader.Views
{
    /// <summary>
    /// Interaction logic for EditDialogView.xaml
    /// </summary>
    public partial class EditDialogView : Window
    {
        public EditDialogView(int sheetId)
        {
            InitializeComponent();
            this.DataContext = new EditViewModel(gridSheet, sheetId);
        }
    }
}
