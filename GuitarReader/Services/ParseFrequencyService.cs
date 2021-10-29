using GuitarReader.Models;
using System;
using System.Collections.Generic;

namespace GuitarReader.Services
{
    class ParseFrequencyService
    {

        public string Parse(int hz)
        {
            Note note = new Note();
            string result = string.Empty;
            
            foreach (string i in note.frequencyDict.Keys)
            {
                if(Math.Abs(note.frequencyDict[i] * 0.97 - hz) <= 7)
                {
                    result = i;
                    break;
                }
            }

            return result;

        }
    }
}
