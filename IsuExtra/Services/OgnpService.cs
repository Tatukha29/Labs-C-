using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Xml.Linq;
using Isu.Classes;
using Isu.Service;
using IsuExtra.Classes;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class OgnpService : IIsuExtraService
    {
        private readonly OgnpRepository _ognpRepository;
        private readonly ScheduleGroupRepository _scheduleGroupRepository;
        private readonly OgnpStudentsRepository _allOgnpStudentsRepository;
        private int _countCourses = 2;
        private int _maxCountStudent = 25;

        public OgnpService(OgnpRepository ognpRepo, ScheduleGroupRepository scheduleGroupRepo, OgnpStudentsRepository allOgnpStudentsRepo)
        {
            _ognpRepository = ognpRepo;
            _scheduleGroupRepository = scheduleGroupRepo;
            _allOgnpStudentsRepository = allOgnpStudentsRepo;
        }

        public Ognp AddOgnp(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new IsuExtraException("Empty naming");
            }

            if (name.Length != 5 && int.Parse(name.Substring(4, 1)) != 3)
            {
                throw new IsuExtraException("Wrong naming");
            }

            string megafaculty = name.Substring(3, 2);
            bool check = false;
            foreach (var checkOgnp in _ognpRepository.GetOgnpList().Where(checkognp => checkognp.Megafaculty == megafaculty))
            {
                check = true;
            }

            if (check == true)
            {
                throw new IsuExtraException("Ognp already exists");
            }

            var ognp = new Ognp(name);
            _ognpRepository.AddOgnpList(ognp);
            return ognp;
        }

        public OgnpCourse AddCourse(string name, Ognp ognp)
        {
            if (ognp.Courses.Count >= _countCourses) throw new IsuExtraException("Too many courses");
            var ognpCourse = new OgnpCourse(name);
            ognp.Courses.Add(ognpCourse);
            return ognpCourse;
        }

        public OgnpGroup AddOgnpGroup(OgnpCourse ognpCourse, string ognpGroupName)
        {
            foreach (Ognp ognp in _ognpRepository.GetOgnpList())
            {
                foreach (OgnpCourse course in ognp.Courses)
                {
                    if (course != ognpCourse) continue;
                    var newOgnpGroup = new OgnpGroup(ognpGroupName);
                    course.OgnpGroups.Add(newOgnpGroup);
                    return newOgnpGroup;
                }
            }

            throw new IsuExtraException("Something went wrong");
        }

        public LessonOgnp AddLessonOgnpGroup(OgnpGroup ognpGroup, DateTime time, DayOfWeek dayOfWeek, string teacher, int room)
        {
            var lessonOgnp = new LessonOgnp(time, dayOfWeek, teacher, room);
            ognpGroup.LessonsOgnp.Add(lessonOgnp);
            return lessonOgnp;
        }

        public ScheduleGroup AddScheduleGroup(Group mainGroup, DateTime time, DayOfWeek dayOfWeek, string teacher, int room)
        {
            if (_scheduleGroupRepository.GetSchedulGroupList().Where(schedule => schedule.Group == mainGroup).Any(schedule => schedule.Time.ToString("t") == time.ToString("t")))
            {
                throw new IsuExtraException("Issues with time");
            }

            var scheduleGroup = new ScheduleGroup(mainGroup, time, dayOfWeek, teacher, room);
            _scheduleGroupRepository.AddSchedulGroupList(scheduleGroup);
            return scheduleGroup;
        }

        public Student AddStudentOgnp(Student student, Ognp ognpName)
        {
            string megafaculty = student.Group.Name.Name.Substring(0, 2);
            if (_ognpRepository.GetOgnpList().Where(ognp => ognp == ognpName).Any(ognp => ognp.Megafaculty == megafaculty))
            {
                throw new IsuExtraException("One megafaculty");
            }

            if (_allOgnpStudentsRepository.GetStudentSOgnpList().Any(students => students == student)) throw new IsuExtraException("Student are already signed up in ognp");

            int check = 0;
            foreach (var scheduleGroup in _scheduleGroupRepository.GetSchedulGroupList())
            {
                if (scheduleGroup.Group != student.Group) continue;
                foreach (var course in _ognpRepository.GetOgnpList().Where(ognp => ognp == ognpName).SelectMany(ognp => ognp.Courses))
                {
                    foreach (var ognpGroup in course.OgnpGroups)
                    {
                        foreach (var lessonOgnp in ognpGroup.LessonsOgnp)
                        {
                            if ((lessonOgnp.Time.ToString("t") != scheduleGroup.Time.ToString("t") || lessonOgnp.DayOfWeek != scheduleGroup.DayOfWeek) && ognpGroup.StudentsOgnp.Count < _maxCountStudent)
                            {
                                ognpGroup.StudentsOgnp.Add(student);
                                check++;
                                break;
                            }
                        }

                        if (ognpGroup.StudentsOgnp.Count == _maxCountStudent)
                        {
                            throw new IsuExtraException("No place");
                        }
                    }
                }
            }

            if (check != 2) throw new IsuExtraException("Error");
            _allOgnpStudentsRepository.AddStudentOgnpList(student);
            return student;
        }

        public void RemoveStudentOgnp(Student student, Ognp ognp)
        {
            _allOgnpStudentsRepository.RemoveStudentOgnpList(student);
            foreach (var course in ognp.Courses)
            {
                foreach (var ognpGroup in course.OgnpGroups)
                {
                    ognpGroup.StudentsOgnp.Remove(student);
                }
            }
        }

        public OgnpCourse GetOgnpCourse(OgnpCourse ognpCourse)
        {
            return _ognpRepository.GetOgnpList().SelectMany(ognp => ognp.Courses).FirstOrDefault(course => course == ognpCourse);
        }

        public OgnpGroup GetOgnpGroup(OgnpGroup ognpGroup)
        {
            return _ognpRepository.GetOgnpList().SelectMany(ognp => ognp.Courses).SelectMany(course => course.OgnpGroups).FirstOrDefault(ognpGroups => ognpGroups == ognpGroup);
        }

        public List<Student> StudentsWithoutOgnpGroup(List<Student> result)
        {
            return result.Except(_allOgnpStudentsRepository.GetStudentSOgnpList()).ToList();
        }
    }
}