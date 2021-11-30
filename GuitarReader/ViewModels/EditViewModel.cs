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

        private DisplayService displayService;
        private NoteService noteService = new NoteService();
        private SheetService sheetService = new SheetService();
        private List<Note> noteList;

        public EditViewModel(Grid gridSheet, int sheetId)
        {
            displayService = new DisplayService(gridSheet);
            noteList = noteService.SelectById(sheetId);
            MoveBackCommand = new RelayCommand(MoveBackExecuteMethod);
            MoveForwardCommand = new RelayCommand(MoveForwardExecuteMethod);
            initNotes();
        }

        private void initNotes()
        {
            displayService.AddTabWithOffset(noteList);
            
        }

        private void MoveBackExecuteMethod(object obj)
        {
            displayService.MoveBack();
        }

        private void MoveForwardExecuteMethod(object obj)
        {
            displayService.MoveForward();
        }

        public void SaveCurrentInfo()
        {
            List<Note> temp = displayService.GetCurrentNotes();

            noteService.UpdateRecentData(temp);
            sheetService.UpdateRecentDate(temp[0].id);

        }
    }
}
