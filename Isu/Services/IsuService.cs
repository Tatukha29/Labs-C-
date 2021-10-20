using System.Collections.Generic;
using System.Linq;
using Isu.Classes;
using Isu.Services;
using Isu.Tools;

namespace Isu.Service
{
    public class IsuService : IIsuService
    {
        private List<Group> _groups = new List<Group>();
        private List<Student> _allstudents = new List<Student>();
        private int _maxAmountStudents = 24;

        public Group AddGroup(string name)
        {
            Group group = new Group(name);
            _groups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Size >= _maxAmountStudents)
            {
                throw new IsuException("No place");
            }

            var student = new Student(name, group);
            _allstudents.Add(student);
            group.Size++;
            return student;
        }

        public Student GetStudent(int id)
        {
            foreach (Student student in _allstudents.Where(student => student.Id == id))
            {
                return student;
            }

            throw new IsuException("Student not found");
        }

        public Student FindStudent(string name)
        {
            return _allstudents.FirstOrDefault(student => student.Name == name);
        }

        public List<Student> FindStudents(string groupName)
        {
            List<Student> studentsGroup = _allstudents.Where(student => student.Group.Name.Name == groupName).ToList();
            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _allstudents.Where(student => student.Group.Name.Course == courseNumber).ToList();
        }

        public Group FindGroup(string groupName)
        {
            return _groups.FirstOrDefault(@group => @group.Name.Name == groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.Where(@group => @group.Name.Course == courseNumber).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            if (newGroup.Size >= _maxAmountStudents)
            {
                throw new IsuException("No place to add student");
            }

            student.Group.Size -= 1;
            student.Group = newGroup;
            student.Group.Size += 1;
        }
    }
}