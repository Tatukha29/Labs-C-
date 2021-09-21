using Isu.Services;

namespace Isu.Classes
{
    public class Student
    {
        private static int studentId = 100000;
        public Student(string name)
        {
            Name = name;
            Id = GenericId();
        }

        public string Name { get; }
        public int Id { get; set; }

        private int GenericId()
        {
            studentId = studentId++;
            return studentId;
        }
    }
}