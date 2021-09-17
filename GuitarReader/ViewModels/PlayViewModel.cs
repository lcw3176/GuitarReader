using GuitarReader.Command;
using GuitarReader.Services;
using System.Windows.Input;

namespace GuitarReader.ViewModels
{
    class PlayViewModel : BaseViewModel
    {
        public ICommand PlayCommand { get; private set; }
        public ICommand StopCommand { get; private set; }

        private PlayService playService = new PlayService();
        private bool isRun = false;

        
        public PlayViewModel()
        {
            PlayCommand = new RelayCommand(PlayExecuteMethod);
            StopCommand = new RelayCommand(StopExecuteMethod);
            
        }


        /// <summary>
        /// 재생 버튼 커맨드
        /// </summary>
        /// <param name="grid">view 그리드 오브젝트</param>
        private void PlayExecuteMethod(object grid)
        {
            isRun = true;
            playService.Play(grid, playId);
        }

        private void StopExecuteMethod(object obj)
        {
            
        }
    }
}
