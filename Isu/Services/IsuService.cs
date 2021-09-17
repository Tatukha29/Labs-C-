using System.Collections.Generic;
using System.Linq;
using Isu.Classes;
using Isu.Services;
using Isu.Tools;

namespace Isu.Service
{
    public class IsuService : IIsuService
    {
        private List<Group> Groups { get; } = new List<Group>();
        private List<Student> Allstudents { get; } = new List<Student>();

// 1
        public Group AddGroup(string name)
        {
            if ((name[0] != 'M') || (name[1] != '3'))
            {
                throw new IsuException("Invalid name of group");
            }

            Group group = new Group(name);
            Groups.Add(group);
            return group;
        }

// 2
        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count > 24)
            {
                throw new IsuException("No place to add student");
            }

            Student student = new Student(name);
            group.Students.Add(student);
            Allstudents.Add(student);
            return student;
        }

// 3
        public Student GetStudent(int id)
        {
            for (int i = 0; i <= Allstudents.Count; i++)
            {
                if (id == Allstudents[i].ID)
                {
                    return Allstudents[i];
                }
            }

            throw new IsuException("student not found");
        }

// 4
        public Student FindStudent(string name)
        {
            for (int i = 0; i <= Allstudents.Count; i++)
            {
                if (name == Allstudents[i].Name)
                {
                    return Allstudents[i];
                }
            }

            return null;
        }

// 5
        public List<Student> FindStudents(string groupName)
        {
            foreach (Group group in Groups)
            {
                if (group.Name.Name == groupName)
                {
                    return group.Students;
                }
            }

            return null;
        }

// 6
        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Student> studentsCourse = new List<Student>();
            foreach (Group group in Groups)
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

// 7
        public Group FindGroup(string groupName)
        {
            foreach (Group group in Groups)
            {
                if (group.Name.Name == groupName)
                {
                    return group;
                }
            }

            return null;
        }

// 8
        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            List<Group> groupCourse = new List<Group>();
            foreach (Group group in Groups)
            {
                if (group.Name.Course == courseNumber)
                {
                    groupCourse.Add(group);
                }
            }

            return groupCourse;
        }

// 9
        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            foreach (Group group in Groups)
            {
                foreach (Student studentik in group.Students.ToList())
                {
                    if (student.ID == studentik.ID)
                    {
                        newGroup.Students.Add(student);
                        group.Students.Remove(student);
                    }
                }
            }
        }
    }
}