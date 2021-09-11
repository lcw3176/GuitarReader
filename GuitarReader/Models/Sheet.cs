using System;
using System.Collections.Generic;

namespace GuitarReader.Models
{
    class Sheet
    {
        public List<Note> notes = new List<Note>();
        public string name;
        public DateTime created;
        public DateTime lastModified;
    }
}
