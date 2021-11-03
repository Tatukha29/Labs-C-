using System.Collections.Generic;

namespace Isu.Classes
{
    public class Group
    {
        public Group(string name)
        {
            Name = new GroupName(name);
            Size = 0;
        }

        public GroupName Name { get; }
        public int Size { get; set; }
    }
}