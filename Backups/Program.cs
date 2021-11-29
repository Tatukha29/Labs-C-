using System.Collections.Generic;
using System.IO;
using Backups.Classes;
using Backups.Services;
using Ionic.Zip;
using Single = Backups.Classes.Single;

namespace Backups
{
    internal class Program
    {
        private static void Main()
        {
            BackupService backupService = new BackupService();
            DirectoryInfo directory = backupService.CreateDirectory(@"D:\backup\qwerty\Second");
            BackupJob backupJob = backupService.CreateBackupJob();
            Repository repository = backupService.CreateRepository(directory);
            JobObject jobObject1 = new JobObject(new FileInfo(@"D:\backup\qwerty\First\FileA"));
            JobObject jobObject2 = new JobObject(new FileInfo(@"D:\backup\qwerty\First\FileB"));
            List<JobObject> jobObjects = new List<JobObject>() { jobObject1, jobObject2 };
            foreach (var jobObject in jobObjects)
            {
                backupJob.AddJobObject(jobObject);
            }

            backupService.StartLocalBackup(algorithm: new Classes.Single(), jobObjects, directory, backupJob, repository);
        }
    }
}
