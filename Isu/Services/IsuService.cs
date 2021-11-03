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
            Student student = _allstudents.Find(student => student.Id.Equals(id));
            if (student == null)
            {
                throw new IsuException("Student not found");
            }

            return student;
        }

        public Student FindStudent(string name)
        {
            return _allstudents.Find(student => student.Name.Equals(name));
        }

        public List<Student> FindStudents(string groupName)
        {
            return _allstudents.FindAll(student => student.Group.Name.Name.Equals(groupName));
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _allstudents.FindAll(student => student.Group.Name.Course.Equals(courseNumber));
        }

        public Group FindGroup(string groupName)
        {
            return _groups.Find(group => group.Name.Name.Equals(groupName));
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _groups.FindAll(group => group.Name.Course == courseNumber);
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