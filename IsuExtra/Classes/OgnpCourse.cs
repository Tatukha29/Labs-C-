using System.Collections.Generic;

namespace IsuExtra.Classes
{
    public class OgnpCourse
    {
        public OgnpCourse(string name)
        {
            Name = name;
            Lessons = new List<Lesson>();
        }

        public string Name { get; }
        public List<Lesson> Lessons { get; }
    }
}