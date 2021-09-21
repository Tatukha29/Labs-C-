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
        private int maxAmountStudents = 24;

        public Group AddGroup(string name)
        {
            if (name[0] < 'A' || name[0] > 'Z')
            {
                throw new IsuException("Invalid name of group");
            }

            if (name[1] != '3')
            {
                throw new IsuException("Invalid name of group");
            }

            Group group = new Group(name);
            _groups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count < maxAmountStudents)
            {
                Student student = new Student(name);
                group.Students.Add(student);
                _allstudents.Add(student);
                return student;
            }
            else
            {
                throw new IsuException("No place to add student");
            }
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
            foreach (Group group in _groups)
            {
                if (group.Name.Name == groupName)
                {
                    return group.Students;
                }
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Student> studentsCourse = new List<Student>();
            foreach (Group group in _groups)
            {
                if (group.Name.Course == courseNumber)
                {
                    foreach (Student student in group.Students)
                    {
                        studentsCourse.Add(student);
                    }
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
            foreach (Group group in _groups)
            {
                foreach (Student item in group.Students)
                {
                    if (student.Id == item.Id)
                    {
                        if (newGroup.Students.Count < maxAmountStudents)
                        {
                            newGroup.Students.Add(student);
                            group.Students.Remove(student);
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
}