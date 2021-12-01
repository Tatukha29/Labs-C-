using System.Collections.Generic;
using System.IO;
using Backups.Classes;

namespace Backups.Services
{
    public class BackupService
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

        public Repository CreateRepository(DirectoryInfo directory)
        {
            Repository repository = new Repository(directory);
            return repository;
        }

        public void StartBackup(IRepository backup, IAlgorithm algorithm, List<JobObject> jobObjects, DirectoryInfo directory, BackupJob backupJob, Repository repository)
        {
            RestorePoint restorePoint = backupJob.CreateRestorePoint();
            List<Storage> storages = backup.MakeBackup(algorithm, jobObjects, restorePoint, directory);
            restorePoint.Storages.AddRange(storages);
        }

        /*public void StartVirtualBackup(IRepository backup, IAlgorithm algorithm, List<JobObject> jobObjects, DirectoryInfo directory, BackupJob backupJob, Repository repository)
        {
            RestorePoint restorePoint = backupJob.CreateRestorePoint();
            List<Storage> storages = backup.MakeBackup(algorithm, backupJob.JobObjects, directory.Name, restorePoint, directory);
            restorePoint.Storages.AddRange(storages);
        }*/
    }
}