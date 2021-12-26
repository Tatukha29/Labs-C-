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
            var ognp = new Ognp(name);
            if (_ognpRepository.GetAll().FirstOrDefault(checkognp => checkognp.Megafaculty == ognp.Megafaculty) != null)
            {
                throw new IsuExtraException("Ognp already exists");
            }

            _ognpRepository.AddOgnp(ognp);
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
            foreach (Ognp ognp in _ognpRepository.GetAll())
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
            _scheduleGroupRepository.AddSchedulGroup(scheduleGroup);
            return scheduleGroup;
        }

        public Student AddStudentOgnp(Student student, Ognp ognpName)
        {
            string megafaculty = student.Group.Name.Name.Substring(0, 2);
            if (_ognpRepository.GetAll().FirstOrDefault(ognp => ognp == ognpName && ognp.Megafaculty == megafaculty) != null)
            {
                throw new IsuExtraException("One megafaculty");
            }

            if (_allOgnpStudentsRepository.GetStudentSOgnpList().Any(students => students == student)) throw new IsuExtraException("Student are already signed up in ognp");

            foreach (var scheduleGroup in _scheduleGroupRepository.GetSchedulGroupList())
            {
                if (scheduleGroup.Group != student.Group) continue;
                foreach (var course in _ognpRepository.GetAll().Where(ognp => ognp == ognpName).SelectMany(ognp => ognp.Courses))
                {
                    foreach (var ognpGroup in course.OgnpGroups)
                    {
                        if (ognpGroup.StudentsOgnp.Count < _maxCountStudent)
                        {
                            foreach (var lessonOgnp in ognpGroup.LessonsOgnp)
                            {
                                if ((lessonOgnp.Time.ToString("t") != scheduleGroup.Time.ToString("t") || lessonOgnp.DayOfWeek != scheduleGroup.DayOfWeek) && ognpGroup.StudentsOgnp.Count < _maxCountStudent)
                                {
                                    ognpGroup.StudentsOgnp.Add(student);
                                    break;
                                }

                                throw new IsuExtraException("You can't sign up this ognp");
                            }
                        }
                        else
                        {
                            throw new IsuExtraException("No place");
                        }
                    }
                }
            }

            _allOgnpStudentsRepository.AddStudentOgnp(student);
            return student;
        }

        public void RemoveStudentOgnp(Student student, Ognp ognp)
        {
            _allOgnpStudentsRepository.RemoveStudentOgnp(student);
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
            return _ognpRepository.GetAll().SelectMany(ognp => ognp.Courses).FirstOrDefault(course => course == ognpCourse);
        }

        public OgnpGroup GetOgnpGroup(OgnpGroup ognpGroup)
        {
            return _ognpRepository.GetAll().SelectMany(ognp => ognp.Courses).SelectMany(course => course.OgnpGroups).FirstOrDefault(ognpGroups => ognpGroups == ognpGroup);
        }

        public List<Student> StudentsWithoutOgnpGroup(List<Student> result)
        {
            return result.Except(_allOgnpStudentsRepository.GetStudentSOgnpList()).ToList();
        }
    }
}