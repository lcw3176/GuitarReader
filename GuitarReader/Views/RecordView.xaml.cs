using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace GuitarReader.Views
{
    /// <summary>
    /// RecordView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RecordView : UserControl
    {
        private Brush brushes = Brushes.Red;
        public RecordView()
        {
            InitializeComponent();
        }

        private void recordButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Brush temp = recordButton.Background;
            recordButton.Background = brushes;
            brushes = temp;
        }
    }
}
