using GuitarReader.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace GuitarReader
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel(); 
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
