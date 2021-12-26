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
        private OgnpRepository _ognpRepository;
        private ScheduleGroupRepository _scheduleGroupRepository;
        private OgnpStudentsRepository _ognpStudentsRepository;

        [SetUp]
        public void Setup()
        {
            _ognpRepository = new OgnpRepository();
            _scheduleGroupRepository = new ScheduleGroupRepository();
            _ognpStudentsRepository = new OgnpStudentsRepository();
            _isuService = new IsuService();
            _isuExtraService = new OgnpService(_ognpRepository, _scheduleGroupRepository, _ognpStudentsRepository);
        }

        [Test]
        public void OgnpAlreadyExists_ThrowException()
        {
            Ognp ognp = _isuExtraService.AddOgnp("КИБM3");
            Assert.Catch<Exception>(() =>
            {
                _isuExtraService.AddOgnp("ФТНM3");
            });
        }

        [Test]
        public void TooManyCourses_ThrowException()
        {
            Ognp ognp = _isuExtraService.AddOgnp("КИБM3");
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
            _isuExtraService.AddScheduleGroup(group, new DateTime(2021, 11, 29, 10, 00, 00), DayOfWeek.Wednesday, "Golovin", 302);
            Ognp ognp = _isuExtraService.AddOgnp("КИБL3");
            OgnpCourse ognpCourseFirst = _isuExtraService.AddCourse("Method", ognp);
            OgnpCourse ognpCourseSecond = _isuExtraService.AddCourse("Cyber", ognp);
            OgnpGroup ognpGroup1 = _isuExtraService.AddOgnpGroup(ognpCourseFirst, "КИБ3.2");
            _isuExtraService.AddLessonOgnpGroup(ognpGroup1, new DateTime(2021, 11, 28, 10, 00, 00), DayOfWeek.Tuesday, "Mayatin", 100);
            OgnpGroup ognpGroup2 = _isuExtraService.AddOgnpGroup(ognpCourseSecond, "POM3.2");
            _isuExtraService.AddLessonOgnpGroup(ognpGroup2, new DateTime(2021, 11, 29, 10, 00, 00), DayOfWeek.Wednesday, "Mayatin", 100);
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
            _isuExtraService.AddScheduleGroup(group, new DateTime(2021, 11, 29, 08, 20, 00), DayOfWeek.Wednesday, "Mayatin", 302);
            Ognp ognp = _isuExtraService.AddOgnp("КИБM3");
            OgnpCourse ognpCourseFirst = _isuExtraService.AddCourse("Method", ognp);
            OgnpCourse ognpCourseSecond = _isuExtraService.AddCourse("Cyber", ognp);
            OgnpGroup ognpGroup1 = _isuExtraService.AddOgnpGroup(ognpCourseFirst, "КИБ3.2");
            _isuExtraService.AddLessonOgnpGroup(ognpGroup1, new DateTime(2021, 11, 28, 10, 00, 00), DayOfWeek.Tuesday, "Mayatin", 100);
            OgnpGroup ognpGroup2 = _isuExtraService.AddOgnpGroup(ognpCourseSecond, "POM3.1");
            _isuExtraService.AddLessonOgnpGroup(ognpGroup2, new DateTime(2021, 11, 29, 10, 00, 00), DayOfWeek.Wednesday, "Mayatin", 100);
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
            _isuExtraService.AddScheduleGroup(group, new DateTime(2021, 11, 27, 10, 00, 00), DayOfWeek.Monday, "Mayatin", 302);
            Ognp ognp = _isuExtraService.AddOgnp("КИБL3");
            OgnpCourse ognpCourseFirst = _isuExtraService.AddCourse("Method", ognp);
            OgnpCourse ognpCourseSecond = _isuExtraService.AddCourse("Cyber", ognp);
            OgnpGroup ognpGroup1 = _isuExtraService.AddOgnpGroup(ognpCourseFirst, "КИБ3.2");
            _isuExtraService.AddLessonOgnpGroup(ognpGroup1, new DateTime(2021, 11, 28, 10, 00, 00), DayOfWeek.Tuesday, "Mayatin", 100);
            OgnpGroup ognpGroup2 = _isuExtraService.AddOgnpGroup(ognpCourseSecond, "POM3.1");
            _isuExtraService.AddLessonOgnpGroup(ognpGroup2, new DateTime(2021, 11, 29, 10, 00, 00), DayOfWeek.Wednesday, "Mayatin", 100);
            _isuExtraService.AddStudentOgnp(student, ognp);
            List<Student> result = _isuService.FindStudents(group.Name.Name);
            Assert.AreEqual(_isuExtraService.StudentsWithoutOgnpGroup(result), new List<Student>() {student2});
        }
    }
}