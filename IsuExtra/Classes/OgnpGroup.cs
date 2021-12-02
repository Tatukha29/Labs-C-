using System;
using System.Collections.Generic;
using System.ComponentModel;
using Isu.Classes;
using IsuExtra.Tools;

namespace IsuExtra.Classes
{
    public class OgnpGroup
    {
        public OgnpGroup(string ognpGroupName, DateTime time, int day, string teacher, int room)
        {
            if (!string.IsNullOrEmpty(ognpGroupName))
            {
                if (ognpGroupName.Length == 6 && ognpGroupName.Substring(4, 1) == "." && int.Parse(ognpGroupName.Substring(5, 1)) > 0 && int.Parse(ognpGroupName.Substring(5, 1)) < 6)
                {
                    LessonOgnp = new LessonOgnp(ognpGroupName, time, day, teacher, room);
                    StudentsOgnp = new List<Student>();
                }
                else
                {
                    throw new IsuExtraException("Wrong naming");
                }
            }
            else
            {
                throw new IsuExtraException("Empty naming");
            }
        }

        public LessonOgnp LessonOgnp { get; }
        public List<Student> StudentsOgnp { get; }
    }
}