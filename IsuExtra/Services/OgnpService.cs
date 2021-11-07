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
        private IsuService _isuService = new IsuService();
        private List<Ognp> _ognps = new List<Ognp>();
        private List<ScheduleGroup> _scheduleGroups = new List<ScheduleGroup>();
        private List<Student> _allOgnpStudents = new List<Student>();

        public Ognp AddOgnp(string name, string megafaculty)
        {
            bool check = false;
            foreach (var checkognp in _ognps.Where(checkognp => checkognp.Megafaculty == megafaculty))
            {
                check = true;
            }

            if (check == true)
            {
                throw new IsuExtraException("Ognp already exists");
            }

            var ognp = new Ognp(name, megafaculty);
            _ognps.Add(ognp);
            return ognp;
        }

        public OgnpCourse AddCourse(string name, Ognp ognp)
        {
            if (ognp.Courses.Count >= 2) throw new IsuExtraException("Too many courses");
            var ognpCourse = new OgnpCourse(name);
            ognp.Courses.Add(ognpCourse);
            return ognpCourse;
        }

        public Lesson AddLesson(OgnpCourse ognpCourse, string lessonName, string time, int size, int day)
        {
            foreach (Ognp ognp in _ognps)
            {
                foreach (OgnpCourse course in ognp.Courses)
                {
                    if (course != ognpCourse) continue;
                    var lesson = new Lesson(lessonName, time, day, size);
                    course.Lessons.Add(lesson);
                    return lesson;
                }
            }

            throw new IsuExtraException("Something went wrong");
        }

        public ScheduleGroup AddScheduleGroup(Group mainGroup, string time, int day)
        {
            if (_scheduleGroups.Where(schedule => schedule.Group == mainGroup).Any(schedule => schedule.Time == time && schedule.Day == day))
            {
                throw new IsuExtraException("Issues with time");
            }

            var scheduleGroup = new ScheduleGroup(mainGroup, time, day);
            _scheduleGroups.Add(scheduleGroup);
            return scheduleGroup;
        }

        public Student AddStudentOgnp(Student student, Ognp ognpName)
        {
            if (_ognps.Where(ognp => ognp == ognpName).Any(ognp => ognp.Megafaculty == student.Group.Name.Name.Substring(0, 2)))
            {
                throw new IsuExtraException("one megafaculty");
            }

            int check = 0;
            foreach (var scheduleGroup in _scheduleGroups)
            {
                if (scheduleGroup.Group != student.Group) continue;
                foreach (var course in _ognps.Where(ognp => ognp == ognpName).SelectMany(ognp => ognp.Courses))
                {
                    foreach (var lesson in course.Lessons)
                    {
                        if ((lesson.Time != scheduleGroup.Time || lesson.Day != scheduleGroup.Day) && lesson.Size > 0)
                        {
                            lesson.Size -= 1;
                            lesson.StudentsOgnp.Add(student);
                            check++;
                            break;
                        }

                        if (lesson.Size == 0)
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
            foreach (Lesson lesson in from ognp in _ognps
                where ognp.Name == ognpName
                from course in ognp.Courses
                from lesson in course.Lessons
                select lesson)
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

        public Lesson GetOgnpGroup(Lesson lesson)
        {
            return (from ognp in _ognps from course in ognp.Courses from lessons in course.Lessons select lessons).FirstOrDefault(lessons => lessons == lesson);
        }

        public Student FindStudentOgnp(Student student)
        {
            foreach (var students in from ognp in _ognps
                from course in ognp.Courses
                from lesson in course.Lessons
                from students in lesson.StudentsOgnp
                where students.Name == student.Name
                select students)
            {
                return students;
            }

            throw new IsuExtraException("Something went wrong");
        }

        public List<Student> StudentsWithoutOgnpGroup(List<Student> result)
        {
            return result.Except(_allOgnpStudents).ToList();
        }
    }
}