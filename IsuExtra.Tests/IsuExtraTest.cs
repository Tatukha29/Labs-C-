
using System;
using System.Collections.Generic;
using Isu.Classes;
using Isu.Service;
using Isu.Services;
using IsuExtra.Classes;
using IsuExtra.Services;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuExtraService _isuExtraService;
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
            _isuExtraService = new OgnpService();
        }

        [Test]
        public void OgnpAlreadyExists_ThrowException()
        {
            Ognp ognp = _isuExtraService.AddOgnp("КИБ", "M3");
            Assert.Catch<Exception>(() =>
            {
                _isuExtraService.AddOgnp("ФТН", "M3");
            });
        }

        [Test]
        public void TooManyCourses_ThrowException()
        {
            Ognp ognp = _isuExtraService.AddOgnp("КИБ", "M3");
            OgnpCourse courseFirst = _isuExtraService.AddCourse("PO", ognp);
            OgnpCourse courseSecond = _isuExtraService.AddCourse("Method", ognp);
            Assert.Catch<Exception>(() =>
            {
                _isuExtraService.AddCourse("Method2", ognp);
            });
        }
        
        [Test]
        public void OgnpAndGroupTimeSame_ThrowException()
        {
            Group group = _isuService.AddGroup("M3208");
            Student student = _isuService.AddStudent(group, "Tanya");
            _isuExtraService.AddScheduleGroup(group, "10:00", 1);
            Ognp ognp = _isuExtraService.AddOgnp("Хуйня", "L3");
            OgnpCourse ognpCourseFirst = _isuExtraService.AddCourse("Method", ognp);
            OgnpCourse ognpCourseSecond = _isuExtraService.AddCourse("Cyber", ognp);
            _isuExtraService.AddLesson(ognpCourseFirst, "PO", "10:00", 20, 1);
            _isuExtraService.AddLesson(ognpCourseSecond, "Cyber", "12:00", 20, 2);
            Assert.Catch<Exception>(() =>
            {
                _isuExtraService.AddStudentOgnp(student, ognp);
            });
        }

        [Test]
        public void OgnpAndGroupMegafacultySame_ThrowException()
        {
            Group group = _isuService.AddGroup("M3208");
            Student student = _isuService.AddStudent(group, "Tanya");
            _isuExtraService.AddScheduleGroup(group, "08:20", 1);
            Ognp ognp = _isuExtraService.AddOgnp("Хуйня", "M3");
            OgnpCourse ognpCourseFirst = _isuExtraService.AddCourse("Method", ognp);
            OgnpCourse ognpCourseSecond = _isuExtraService.AddCourse("Cyber", ognp);
            _isuExtraService.AddLesson(ognpCourseFirst, "PO", "10:00", 20, 1);
            _isuExtraService.AddLesson(ognpCourseSecond, "Cyber", "12:00", 20, 2);
            Assert.Catch<Exception>(() =>
            {
                _isuExtraService.AddStudentOgnp(student, ognp);
            });
        }

        [Test]
        public void StudentsWithoutOgnpGroup()
        {
            Group group = _isuService.AddGroup("M3208");
            Student student = _isuService.AddStudent(group, "Tanya");
            Student student2 = _isuService.AddStudent(group, "Masha");
            _isuExtraService.AddScheduleGroup(group, "08:20", 1);
            Ognp ognp = _isuExtraService.AddOgnp("Хуйня", "L3");
            OgnpCourse ognpCourseFirst = _isuExtraService.AddCourse("Method", ognp);
            OgnpCourse ognpCourseSecond = _isuExtraService.AddCourse("Cyber", ognp);
            _isuExtraService.AddLesson(ognpCourseFirst, "PO", "10:00", 20, 1);
            _isuExtraService.AddLesson(ognpCourseSecond, "Cyber", "12:00", 20, 2);
            _isuExtraService.AddStudentOgnp(student, ognp);
            List<Student> result = _isuService.FindStudents(group.Name.Name);
            Assert.AreEqual(_isuExtraService.StudentsWithoutOgnpGroup(result), new List<Student>() {student2});
        }
    }
}