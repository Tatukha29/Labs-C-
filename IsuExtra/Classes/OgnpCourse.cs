using System.Collections.Generic;

namespace IsuExtra.Classes
{
    public class OgnpCourse
    {
        public OgnpCourse(string name)
        {
            Name = name;
            OgnpGroups = new List<OgnpGroup>();
        }

        public string Name { get; }
        public List<OgnpGroup> OgnpGroups { get; }
    }
}