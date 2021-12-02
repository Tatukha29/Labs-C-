using System.Collections.Generic;
using Isu.Classes;

namespace IsuExtra.Classes
{
    public class OgnpStudentsRepository
    {
        private List<Student> _allOgnpStudents = new List<Student>();

        public List<Student> AddStudentOgnpList(Student student)
        {
            _allOgnpStudents.Add(student);
            return _allOgnpStudents;
        }

        public List<Student> RemoveStudentOgnpList(Student student)
        {
            _allOgnpStudents.Remove(student);
            return _allOgnpStudents;
        }

        public List<Student> GetStudentSOgnpList()
        {
            return _allOgnpStudents;
        }
    }
}