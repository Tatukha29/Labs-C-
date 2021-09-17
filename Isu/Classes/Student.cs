using Isu.Services;

namespace Isu.Classes
{
    public class Student
    {
        private static int studentID = 100000;
        public Student(string name)
        {
            Name = name;
            ID = studentID++;
        }

        public string Name { get; }
        public int ID { get; }
    }
}