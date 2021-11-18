using System;
using System.Collections.Generic;
using System.ComponentModel;
using Isu.Classes;

namespace IsuExtra.Classes
{
    public class OgnpGroup
    {
        public OgnpGroup(string name, DateTime time, string teacher, int room)
        {
            if (name.Length == 6 && !string.IsNullOrEmpty(name) && name.Substring(4, 1) == "." && int.Parse(name.Substring(5, 1)) > 0 && int.Parse(name.Substring(5, 1)) < 6)
            {
                Name = new LessonOgnp(name, time, teacher, room);
                StudentsOgnp = new List<Student>();
            }
        }

        public LessonOgnp Name { get; }
        public DateTime Time { get; }
        public string Teacher { get; }
        public int Room { get; }
        public List<Student> StudentsOgnp { get; }
    }
}