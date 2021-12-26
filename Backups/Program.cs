using System.Collections.Generic;
using System.IO;
using Backups.Classes;
using Backups.Services;
using Ionic.Zip;
using Single = Backups.Classes.SingleAlgorithm;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            BackupService backupService = new BackupService();
            DirectoryInfo directory = backupService.CreateDirectory(@"D:\backup\qwerty\Second");
            string nameRestorePoint = "restore point1";
            BackupJob backupJob = backupService.CreateBackupJob();
            IRepository repository = new LocalRepository(directory);
            JobObject jobObject1 = new JobObject(new FileInfo(@"D:\backup\qwerty\First\FileA"));
            JobObject jobObject2 = new JobObject(new FileInfo(@"D:\backup\qwerty\First\FileB"));
            List<JobObject> jobObjects = new List<JobObject>() { jobObject1, jobObject2 };
            foreach (var jobObject in jobObjects)
            {
                backupJob.AddJobObject(jobObject);
            }

            backupService.StartBackup(repository, new SingleAlgorithm(), jobObjects, directory, backupJob, nameRestorePoint);
        }
    }
}
