using GuitarReader.Command;
using GuitarReader.Models;
using GuitarReader.Services;
using GuitarReader.Util;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace GuitarReader.ViewModels
{
    internal class EditViewModel : BaseViewModel
    {
        public ICommand MoveBackCommand { get; set; }
        public ICommand MoveForwardCommand { get; set; }

        private DisplayUtil displayUtil;
        private NoteService noteService = new NoteService();
        private List<Note> noteList;

        public EditViewModel(Grid gridSheet, int sheetId)
        {
            displayUtil = new DisplayUtil(gridSheet);
            noteList = noteService.ReadById(sheetId);
            MoveBackCommand = new RelayCommand(MoveBackExecuteMethod);
            MoveForwardCommand = new RelayCommand(MoveForwardExecuteMethod);
            initNotes();
        }

        private void initNotes()
        {
            displayUtil.AddTabWithOffset(noteList);
            
        }

        private void MoveBackExecuteMethod(object obj)
        {
            displayUtil.MoveBack();
        }

        private void MoveForwardExecuteMethod(object obj)
        {
            displayUtil.MoveForward();
        }

        public void SaveCurrentInfo()
        {
            List<Note> temp = displayUtil.GetCurrentNotes();

            for(int i = 0; i < temp.Count; i++)
            {
                if(temp[i].beatLen != noteList[i].beatLen)
                {
                    noteService.UpdateBeatLength(noteList[i], temp[i]);
                }
            }

        }
    }
}
