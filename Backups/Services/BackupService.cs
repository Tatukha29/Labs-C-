using System.Collections.Generic;
using System.IO;
using Backups.Classes;

namespace Backups.Services
{
    public class BackupService : IBackupService
    {
        public DirectoryInfo CreateDirectory(string path)
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            if (!directory.Exists)
            {
                directory.Create();
            }

            return directory;
        }

        public BackupJob CreateBackupJob()
        {
            BackupJob backupJob = new BackupJob();
            return backupJob;
        }

        public void StartBackup(IRepository backup, IAlgorithm algorithm, List<JobObject> jobObjects, DirectoryInfo directory, BackupJob backupJob)
        {
            RestorePoint restorePoint = backupJob.CreateRestorePoint();
            List<Storage> storages = backup.MakeBackup(algorithm, jobObjects, restorePoint);
            restorePoint.Storages.AddRange(storages);
        }
    }
}