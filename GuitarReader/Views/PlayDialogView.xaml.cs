using GuitarReader.Util;
using GuitarReader.ViewModels;
using System.Windows;

namespace GuitarReader.Views
{
    /// <summary>
    /// PlayView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PlayView : Window
    {
        public PlayView(int playId)
        {
            InitializeComponent();
            this.DataContext = new PlayViewModel(gridSheet, playId);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
        }
    }
}
