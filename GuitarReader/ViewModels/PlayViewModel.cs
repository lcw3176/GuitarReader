using GuitarReader.Command;
using GuitarReader.Services;
using GuitarReader.Util;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace GuitarReader.ViewModels
{
    class PlayViewModel : BaseViewModel
    {
        public ICommand PlayCommand { get; private set; }
        public ICommand PauseCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        private bool isRun = false;

        public bool IsRun
        {
            get { return isRun; }
            set
            {
                isRun = value;
                OnPropertyChanged("IsRun");
            }
        }

        private PlayUtil playUtil = new PlayUtil();
        private AnimUtil animUtil;
        private int playId;

        public PlayViewModel(Grid _gridSheet, int _playId)
        {
            PlayCommand = new RelayCommand(PlayExecuteMethod);
            PauseCommand = new RelayCommand(PauseExecuteMethod);
            StopCommand = new RelayCommand(StopExecuteMethod);
            animUtil = new AnimUtil(_gridSheet);

            playUtil.beatPlayEvent += (stringPos, fretPos) => animUtil.AddTab(stringPos, fretPos);
            playUtil.beatEndEvent += () => { IsRun = false; };

            playId = _playId;
        }


        /// <summary>
        /// 재생 버튼 커맨드
        /// </summary>
        /// <param name="obj"></param>
        private void PlayExecuteMethod(object obj)
        {
            IsRun = true;
            playUtil.Play(playId);
            animUtil.Resume();
        }


        /// <summary>
        /// 일시 정지 커맨드
        /// </summary>
        /// <param name="obj"></param>
        private void PauseExecuteMethod(object obj)
        {
            IsRun = false;
            playUtil.Pause();
            animUtil.Pause();
        }


        /// <summary>
        /// 정지 커맨드
        /// </summary>
        /// <param name="obj"></param>
        private void StopExecuteMethod(object obj)
        {
            IsRun = false;
            playUtil.Stop();
            animUtil.Stop();
        }

    }
}
