using GuitarReader.Models;
using GuitarReader.Repository;
using System.Collections.Generic;

namespace GuitarReader.Services
{
    class SheetService
    {
        private SheetRepository sheetRepository = new SheetRepository();

        public void Insert(Sheet sheet)
        {
            sheetRepository.Insert(sheet);
        }
        

        public List<Sheet> ReadAll()
        {
            return sheetRepository.ReadAll();
        }

        public Sheet ReadByName(string sheetName)
        {
            return sheetRepository.ReadByName(sheetName);
        }

        public Sheet ReadMostRecent()
        {
            return sheetRepository.ReadMostRecent();
        }

        public void UpdateRecentDate(int id)
        {
            sheetRepository.UpdateRecentDate(id);
        }
    }
}
