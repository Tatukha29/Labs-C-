namespace Isu.Classes
{
    public class GroupName
    {
        public GroupName(string name)
        {
            Name = name;
            Course = (CourseNumber)int.Parse(name.Substring(2, 1));
            Group = int.Parse(name.Substring(3, 2));
        }

        public string Name { get; }
        public CourseNumber Course { get; }
        public int Group { get; }
    }
}