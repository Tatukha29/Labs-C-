using System.Security.Principal;

namespace Isu.Classes
{
    public class StudentId
    {
        private static int _studentId;

        public StudentId()
        {
            Id = ++_studentId;
        }

        public int Id { get; }
    }
}