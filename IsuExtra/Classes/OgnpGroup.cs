using System;
using System.Collections.Generic;
using System.ComponentModel;
using Isu.Classes;
using IsuExtra.Tools;

namespace IsuExtra.Classes
{
    public class OgnpGroup
    {
        public OgnpGroup(string ognpGroupName)
        {
            if (!string.IsNullOrEmpty(ognpGroupName))
            {
                if (ognpGroupName.Length == 6 && ognpGroupName.Substring(4, 1) == "." && int.Parse(ognpGroupName.Substring(5, 1)) > 0 && int.Parse(ognpGroupName.Substring(5, 1)) < 6)
                {
                    Name = ognpGroupName;
                    StudentsOgnp = new List<Student>();
                    LessonsOgnp = new List<LessonOgnp>();
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

        public string Name { get; }
        public List<Student> StudentsOgnp { get; }
        public List<LessonOgnp> LessonsOgnp { get; }
    }
}