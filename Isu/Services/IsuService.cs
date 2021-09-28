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
        private List<string> _faculties = new List<string>() { "M3", "L3" };

        public Group AddGroup(string name)
        {
            bool check = false;
            string n = name.Substring(0, 2);
            foreach (var faculty in _faculties)
            {
                if (faculty == n)
                {
                    check = true;
                }
            }

            if (check == false)
            {
                throw new IsuException("Invalid name of group");
            }

            var group = new Group(name);
            _groups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
                Student student = new Student(name, group);
                if (student.Group.Size > _maxAmountStudents)
                {
                    throw new IsuException("No place in group");
                }

                _allstudents.Add(student);
                group.Size++;
                return student;
        }

        public Student GetStudent(int id)
        {
            foreach (Student student in _allstudents)
            {
                if (student.Id == id)
                {
                    return student;
                }
            }

            throw new IsuException("Student not found");
        }

        public Student FindStudent(string name)
        {
            foreach (Student student in _allstudents)
            {
                if (student.Name == name)
                {
                    return student;
                }
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            List<Student> studentsGroup = new List<Student>();
            foreach (Student student in _allstudents)
            {
                if (student.Group.Name.Name == groupName)
                {
                    studentsGroup.Add(student);
                }
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Student> studentsCourse = new List<Student>();
            foreach (Student student in _allstudents)
            {
                if (student.Group.Name.Course == courseNumber)
                {
                        studentsCourse.Add(student);
                }
            }

            return studentsCourse;
        }

        public Group FindGroup(string groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.Name.Name == groupName)
                {
                    return group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            List<Group> groupCourse = new List<Group>();
            foreach (Group group in _groups)
            {
                if (group.Name.Course == courseNumber)
                {
                    groupCourse.Add(group);
                }
            }

            return groupCourse;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (Student item in _allstudents)
            {
                    if (student.Id == item.Id)
                    {
                        if (newGroup.Size < _maxAmountStudents)
                        {
                            student.Group = newGroup;
                        }
                        else
                        {
                            throw new IsuException("No place to add student");
                        }
                    }
            }
        }
    }
}