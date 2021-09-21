using Isu.Classes;
using Isu.Service;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3208");
            Student student = _isuService.AddStudent(group,"Tanya");
            Assert.Contains(student, group.Students);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3200");
                for (int i = 1; i <= 25; ++i)
                {
                    _isuService.AddStudent(group, "Vanya");
                }
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("!3333");
                Group group2 = _isuService.AddGroup("M7302");
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group group = _isuService.AddGroup("M3208");
                Student student = _isuService.AddStudent(group, "Masha");
                Group group2 = _isuService.AddGroup("M3200");
                for (int i = 0; i <= 23; ++i)
                {
                    _isuService.AddStudent(group2, "Tanya");
                } 
                _isuService.ChangeStudentGroup(student, group2);
            });
        }
    }
}