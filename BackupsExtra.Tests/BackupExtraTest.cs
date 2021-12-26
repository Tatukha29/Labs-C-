using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading;
using BackupExtra.Classes;
using Backups.Classes;
using Backups.Services;
using BackupsExtra.Classes;
using BackupsExtra.Services;
using NUnit.Framework;
using ILogger = NUnit.Framework.Internal.ILogger;

namespace BackupsExtra.Tests
{
    public class BackupExtraTest
    {
        public class BackupsExtraTest
        {
            private IBackupService _backupService;
            private ILogger _logger;
            private IRecovery _recovery;
            private IDeleteAlgorithm _deleteAlgorithm;
        
            [SetUp]
            public void Setup()
            {
                _backupService = new BackupService();
            }

            [Test]
            public void AlgorithmCountRestorePoints()
            {
                BackupExtraService backupExtraService = new BackupExtraService();
                DirectoryInfo directory = _backupService.CreateDirectory(@"D:\..\..\..\Second");
                string nameRestorePoint1 = "restore point1";
                string nameRestorePoint2 = "restore point2";
                string nameRestorePoint3 = "restore point3";
                string nameRestorePoint4 = "restore point4";
                BackupJob backupJob = _backupService.CreateBackupJob();
                IRepository repository = new VirtualRepository(directory);
                JobObject jobObject1 = new JobObject(new FileInfo(@"D:\..\..\..\..\FileA"));
                JobObject jobObject2 = new JobObject(new FileInfo(@"D:\..\..\..\..\FileB"));
                List<JobObject> jobObjects = new List<JobObject>() { jobObject1, jobObject2 };
                foreach (var jobObject in jobObjects)
                {
                    backupJob.AddJobObject(jobObject);
                }

                _backupService.StartBackup(repository, new SplitAlgorithm(), jobObjects, directory, backupJob, nameRestorePoint1);
                Thread.Sleep(1000);
                _backupService.StartBackup(repository, new SplitAlgorithm(), jobObjects, directory, backupJob, nameRestorePoint2);
                Thread.Sleep(1000);
                _backupService.StartBackup(repository, new SplitAlgorithm(), jobObjects, directory, backupJob, nameRestorePoint3);
                Thread.Sleep(1000);
                _backupService.StartBackup(repository, new SplitAlgorithm(), jobObjects, directory, backupJob, nameRestorePoint4);
                Thread.Sleep(1000);
                List<RestorePoint> restorePoints = backupExtraService.DeleteRestorePoints(new CountAlgorithm(), backupJob, 2, DateTime.Today);
                Assert.AreEqual(2, restorePoints.Count);
            }
            
            [Test]
            public void AlgorithmHybridRestorePoints()
            {
                BackupExtraService backupExtraService = new BackupExtraService();
                DirectoryInfo directory = _backupService.CreateDirectory(@"D:\..\..\..\Second");
                string nameRestorePoint1 = "restore point1";
                string nameRestorePoint2 = "restore point2";
                string nameRestorePoint3 = "restore point3";
                string nameRestorePoint4 = "restore point4";
                BackupJob backupJob = _backupService.CreateBackupJob();
                IRepository repository = new VirtualRepository(directory);
                JobObject jobObject1 = new JobObject(new FileInfo(@"D:\..\..\..\..\FileA"));
                JobObject jobObject2 = new JobObject(new FileInfo(@"D:\..\..\..\..\FileB"));
                List<JobObject> jobObjects = new List<JobObject>() { jobObject1, jobObject2 };
                foreach (var jobObject in jobObjects)
                {
                    backupJob.AddJobObject(jobObject);
                }

                _backupService.StartBackup(repository, new SplitAlgorithm(), jobObjects, directory, backupJob, nameRestorePoint1);
                Thread.Sleep(1000);
                _backupService.StartBackup(repository, new SplitAlgorithm(), jobObjects, directory, backupJob, nameRestorePoint2);
                Thread.Sleep(1000);
                _backupService.StartBackup(repository, new SplitAlgorithm(), jobObjects, directory, backupJob, nameRestorePoint3);
                Thread.Sleep(1000);
                _backupService.StartBackup(repository, new SplitAlgorithm(), jobObjects, directory, backupJob, nameRestorePoint4);
                Thread.Sleep(1000);
                List<RestorePoint> restorePoints = backupExtraService.DeleteRestorePoints(new HybridOneAlgorithm(), backupJob, 2, DateTime.Now) ;
                Assert.AreEqual(2, restorePoints.Count);
            }
            
            [Test]
            public void MergeRestorePoint()
            {
                BackupExtraService backupExtraService = new BackupExtraService();
                DirectoryInfo directory = _backupService.CreateDirectory(@"D:\..\..\..\Second");
                string nameRestorePoint1 = "restore point1";
                string nameRestorePoint2 = "restore point2";
                string nameRestorePoint3 = "restore point3";
                string nameRestorePoint4 = "restore point4";
                BackupJob backupJob = _backupService.CreateBackupJob();
                IRepository repository = new VirtualRepository(directory);
                JobObject jobObject1 = new JobObject(new FileInfo(@"D:\..\..\..\..\FileA"));
                JobObject jobObject2 = new JobObject(new FileInfo(@"D:\..\..\..\..\FileB"));
                List<JobObject> jobObjects = new List<JobObject>() { jobObject1, jobObject2 };
                foreach (var jobObject in jobObjects)
                {
                    backupJob.AddJobObject(jobObject);
                }

                _backupService.StartBackup(repository, new SplitAlgorithm(), jobObjects, directory, backupJob, nameRestorePoint1);
                backupJob.JobObjects.Remove(jobObject1);
                jobObjects.Remove(jobObject1);
                _backupService.StartBackup(repository, new SplitAlgorithm(), jobObjects, directory, backupJob, nameRestorePoint2);
                MergeRestorePoints merge = new MergeRestorePoints();
                merge.UpdateMissingJobObjects(backupJob);
                Assert.AreEqual(2, backupJob.RestorePoints.Last().Storages.Count);
                
            }
        }
    }
}