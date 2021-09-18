using GuitarReader.ViewModels;
using System.Windows.Controls;

namespace GuitarReader.Views
{
    /// <summary>
    /// PlayView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PlayView : UserControl
    {
        public PlayView()
        {
            InitializeComponent();
            this.DataContext = new PlayViewModel(gridSheet);
        }
    }
}
