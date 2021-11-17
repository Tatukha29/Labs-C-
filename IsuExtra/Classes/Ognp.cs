using System.Collections.Generic;

namespace IsuExtra.Classes
{
    public class Ognp
    {
        public Ognp(string name)
        {
            Id = new OgnpId().Id;
            Name = name.Substring(0, 3);
            Megafaculty = name.Substring(3, 2);
            Courses = new List<OgnpCourse>();
        }

        public int Id { get; }
        public string Name { get; }
        public string Megafaculty { get; }
        public List<OgnpCourse> Courses { get; }
    }
}