using GuitarReader.Models;
using System;

namespace GuitarReader.Util
{
    class ParseFrequencyService
    {
        private Note note = new Note();
        public string Parse(int hz)
        {
            int minValue = 10000;
            string result = string.Empty;

            foreach (string i in note.frequencyDict.Keys)
            {
                if (Math.Abs(note.frequencyDict[i] - hz) < minValue)
                {
                    minValue = Math.Abs(note.frequencyDict[i] - hz);
                    result = i;
                }
            }

            return result;

        }
    }
}
