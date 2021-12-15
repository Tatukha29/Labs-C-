using System.Collections.Generic;
using Isu.Classes;

namespace IsuExtra.Classes
{
    public class OgnpStudentsRepository
    {
        private List<Student> _allOgnpStudents = new List<Student>();

        public Student AddStudentOgnp(Student student)
        {
            _allOgnpStudents.Add(student);
            return student;
        }

        public void RemoveStudentOgnp(Student student)
        {
            _allOgnpStudents.Remove(student);
        }

        public List<Student> GetStudentSOgnpList()
        {
            List<Student> newAllStudents = new List<Student>();
            newAllStudents.AddRange(_allOgnpStudents);
            return newAllStudents;
        }
    }
}