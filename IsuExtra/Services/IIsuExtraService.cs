using System;
using System.Collections.Generic;
using Isu.Classes;
using IsuExtra.Classes;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        Ognp AddOgnp(string name, string megafaculty);
        OgnpCourse AddCourse(string name, Ognp ognp);
        Lesson AddLesson(OgnpCourse ognpCourse, string lessonName, string time, int size, int day);
        ScheduleGroup AddScheduleGroup(Group mainGroup, string time, int day);
        Student AddStudentOgnp(Student student, Ognp ognpName);
        void RemoveStudentOgnp(Student student, string ognpName);
        OgnpCourse GetOgnpCourse(OgnpCourse ognpCourse);
        Lesson GetOgnpGroup(Lesson lesson);
        Student FindStudentOgnp(Student student);
        List<Student> StudentsWithoutOgnpGroup(List<Student> result);
    }
}