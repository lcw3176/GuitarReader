using GuitarReader.Models;
using System;

namespace GuitarReader.Services
{
    class ParseFrequencyService
    {
        private Note note = new Note();
        public string Parse(int hz)
        {

            string result = string.Empty;
            
            foreach (string i in note.frequencyDict.Keys)
            {
                if(Math.Abs(note.frequencyDict[i] * 0.95 - hz) <= 7)
                {
                    result = i;
                    break;
                }
            }

            return result;

        }
    }
}
