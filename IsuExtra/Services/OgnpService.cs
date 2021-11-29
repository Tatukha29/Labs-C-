using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using Isu.Classes;
using Isu.Service;
using IsuExtra.Classes;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class OgnpService : IIsuExtraService
    {
        private List<Ognp> _ognps = new List<Ognp>();
        private List<ScheduleGroup> _scheduleGroups = new List<ScheduleGroup>();
        private List<Student> _allOgnpStudents = new List<Student>();
        private int _countCourses = 2;
        private int _maxCountStudent = 25;

        public Ognp AddOgnp(string name)
        {
            string megafaculty = name.Substring(3, 2);
            bool check = false;
            foreach (var checkognp in _ognps.Where(checkognp => checkognp.Megafaculty == megafaculty))
            {
                check = true;
            }

            if (check == true)
            {
                throw new IsuExtraException("Ognp already exists");
            }

            var ognp = new Ognp(name);
            _ognps.Add(ognp);
            return ognp;
        }

        public OgnpCourse AddCourse(string name, Ognp ognp)
        {
            if (ognp.Courses.Count >= _countCourses) throw new IsuExtraException("Too many courses");
            var ognpCourse = new OgnpCourse(name);
            ognp.Courses.Add(ognpCourse);
            return ognpCourse;
        }

        public OgnpGroup AddOgnpGroup(OgnpCourse ognpCourse, string groupName, DateTime time, string teacher, int room)
        {
            foreach (Ognp ognp in _ognps)
            {
                foreach (OgnpCourse course in ognp.Courses)
                {
                    if (course != ognpCourse) continue;
                    var newOgnpGroup = new OgnpGroup(groupName, time, teacher, room);
                    course.OgnpGroups.Add(newOgnpGroup);
                    return newOgnpGroup;
                }
            }

            throw new IsuExtraException("Something went wrong");
        }

        public ScheduleGroup AddScheduleGroup(Group mainGroup, DateTime time, string teacher, int room)
        {
            if (_scheduleGroups.Where(schedule => schedule.Group == mainGroup).Any(schedule => schedule.Time == time))
            {
                throw new IsuExtraException("Issues with time");
            }

            var scheduleGroup = new ScheduleGroup(mainGroup, time, teacher, room);
            _scheduleGroups.Add(scheduleGroup);
            return scheduleGroup;
        }

        public Student AddStudentOgnp(Student student, Ognp ognpName)
        {
            string megafaculty = student.Group.Name.Name.Substring(0, 2);
            if (_ognps.Where(ognp => ognp == ognpName).Any(ognp => ognp.Megafaculty == megafaculty))
            {
                throw new IsuExtraException("one megafaculty");
            }

            if (_allOgnpStudents.Any(students => students == student)) throw new IsuExtraException("you are already signed up");

            int check = 0;
            foreach (var scheduleGroup in _scheduleGroups)
            {
                if (scheduleGroup.Group != student.Group) continue;
                foreach (var course in _ognps.Where(ognp => ognp == ognpName).SelectMany(ognp => ognp.Courses))
                {
                    foreach (var ognpGroup in course.OgnpGroups)
                    {
                        if ((ognpGroup.Name.Time != scheduleGroup.Time) && ognpGroup.StudentsOgnp.Count < _maxCountStudent)
                        {
                            ognpGroup.StudentsOgnp.Add(student);
                            check++;
                            break;
                        }

                        if (ognpGroup.StudentsOgnp.Count == _maxCountStudent)
                        {
                            throw new IsuExtraException("No place");
                        }
                    }
                }
            }

            if (check != 2) throw new IsuExtraException("Error");
            _allOgnpStudents.Add(student);
            return student;
        }

        public void RemoveStudentOgnp(Student student, string ognpName)
        {
            foreach (OgnpGroup lesson in from ognp in _ognps
                where ognp.Name == ognpName
                from course in ognp.Courses
                from ognpGroup in course.OgnpGroups
                select ognpGroup)
            {
                if (lesson.StudentsOgnp.Any(ognpStudent => ognpStudent.Name == student.Name))
                {
                    lesson.StudentsOgnp.Remove(student);
                }
            }
        }

        public OgnpCourse GetOgnpCourse(OgnpCourse ognpCourse)
        {
            return _ognps.SelectMany(ognp => ognp.Courses).FirstOrDefault(course => course == ognpCourse);
        }

        public OgnpGroup GetOgnpGroup(OgnpGroup ognpGroup)
        {
            return (from ognp in _ognps from course in ognp.Courses from ognpGroups in course.OgnpGroups select ognpGroups).FirstOrDefault(ognpGroups => ognpGroups == ognpGroup);
        }

        public List<Student> StudentsWithoutOgnpGroup(List<Student> result)
        {
            return result.Except(_allOgnpStudents).ToList();
        }
    }
}