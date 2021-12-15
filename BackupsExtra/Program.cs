using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Classes;
using Backups.Services;
using BackupsExtra.Classes;
using BackupsExtra.Services;

namespace BackupsExtra
{
    internal class Program
    {
        private static void Main()
        {
            BackupService backupService = new BackupService();
            BackupExtraService backupExtraService = new BackupExtraService();
            DirectoryInfo directory = backupService.CreateDirectory(@"D:\backup\qwerty\Second");
            BackupJob backupJob = backupService.CreateBackupJob();
            IRepository repository = new LocalRepository(directory);
            JobObject jobObject1 = new JobObject(new FileInfo(@"D:\backup\qwerty\First\FileA"));
            ConsoleLoger consoleLoger = new ConsoleLoger();
            backupExtraService.Logger(new ConsoleLoger(), DateTime.Now, " Add job object");
            JobObject jobObject2 = new JobObject(new FileInfo(@"D:\backup\qwerty\First\FileB"));
            consoleLoger.Loger(DateTime.Now, " Add job object");
            List<JobObject> jobObjects = new List<JobObject>() { jobObject1, jobObject2 };
            foreach (var jobObject in jobObjects)
            {
                backupJob.AddJobObject(jobObject);
            }

            Json json = new Json();
            json.Cerialize(backupJob);
            json.Decerialize("D:\\oopitmo\\BackupsExtra\\bin\\Debug\\netcoreapp3.1\\tanya.json");
        }
    }
}