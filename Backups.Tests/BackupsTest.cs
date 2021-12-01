using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Classes;
using Backups.Services;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTest
    {
        private IBackupService _backupService;
        
        [SetUp]
        public void Setup()
        {
            BackupService _backupService = new BackupService();
        }

        [Test]
        public void CheckTwoRestorePointAndThreeStorage()
        {
            BackupService backupService = new BackupService();
            DirectoryInfo directory = backupService.CreateDirectory(@"../../../Second");
            BackupJob backupJob = backupService.CreateBackupJob();
            Repository repository = backupService.CreateRepository(directory);
            JobObject jobObject1 = new JobObject(new FileInfo(@"../../../../FileA"));
            JobObject jobObject2 = new JobObject(new FileInfo(@"../../../../FileB"));
            List<JobObject> jobObjects = new List<JobObject>() { jobObject1, jobObject2 };
            foreach (JobObject jobObject in jobObjects)
            {
                backupJob.AddJobObject(jobObject);
            }
            backupService.StartBackup(new Virtual(), algorithm: new Split(), backupJob.JobObjects, directory, backupJob, repository);
            backupJob.RemoveJobObject(jobObject1);
            backupService.StartBackup(new Virtual(), algorithm: new Split(), backupJob.JobObjects, directory, backupJob, repository);
            var check = 0;
            foreach (var restorePoint in backupJob.RestorePoints)
            {
                check += restorePoint.Storages.Count;
            }
            Assert.AreEqual(2, backupJob.RestorePoints.Count);
            Assert.AreEqual(3, check);
        }
    }
}