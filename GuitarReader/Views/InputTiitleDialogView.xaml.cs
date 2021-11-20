using System.Windows;

namespace GuitarReader.Views
{
    /// <summary>
    /// InputTiitleDialog.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InputTiitleDialogView : Window
    {
        public InputTiitleDialogView()
        {
            InitializeComponent();
        }

        private void confirm_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
