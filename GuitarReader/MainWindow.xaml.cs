using GuitarReader.Repository;
using GuitarReader.Services;
using GuitarReader.ViewModels;
using System.Windows;
using System.Windows.Input;
using Toub.Sound.Midi;

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
            MidiPlayer.OpenMidi();
            MidiPlayer.Play(new ProgramChange(0, 0, GeneralMidiInstruments.SteelAcousticGuitar));
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                return;
            }

            DragMove();

        }


        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            MidiPlayer.CloseMidi();
            BaseRepository.CloseConnection();
        }
    }
}
