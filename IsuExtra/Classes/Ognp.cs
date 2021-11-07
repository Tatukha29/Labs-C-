using System.Collections.Generic;

namespace IsuExtra.Classes
{
    public class Ognp
    {
        public Ognp(string name, string megafaculty)
        {
            Id = new OgnpId().Id;
            Name = name;
            Megafaculty = megafaculty;
            Courses = new List<OgnpCourse>();
        }

        public int Id { get; }
        public string Name { get; }
        public string Megafaculty { get; }
        public List<OgnpCourse> Courses { get; }
    }
}