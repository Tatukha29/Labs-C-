using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Service;
using Isu.Tools;

namespace Isu.Classes
{
    public class GroupName
    {
        private List<string> _faculty = new List<string>() { "M3", "L3" };
        public GroupName(string name)
        {
            foreach (string faculty in _faculty.Where(faculty => name.Substring(0, 2) == faculty && name.Length == 5 && !string.IsNullOrEmpty(name)))
            {
                Name = name;
                Course = (CourseNumber)int.Parse(name.Substring(2, 1));
                Group = int.Parse(name.Substring(3, 2));
            }

            throw new IsuException("Invalid group name");
        }

        public string Name { get; }
        public CourseNumber Course { get; }
        public int Group { get; }
    }
}