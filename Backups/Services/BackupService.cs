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

        public void StartLocalBackup(IAlgorithm algorithm, List<JobObject> jobObjects, DirectoryInfo directory, BackupJob backupJob, Repository repository)
        {
            RestorePoint restorePoint = backupJob.CreateRestorePoint();
            List<Storage> storages = repository.MakeLocalBackUp(algorithm, backupJob.JobObjects, directory.Name, restorePoint);
            foreach (Storage storage in storages)
            {
                restorePoint.Storage.Add(storage);
            }
        }

        public void StartVirtualBackup(IAlgorithm algorithm, List<JobObject> jobObjects, DirectoryInfo directory, BackupJob backupJob, Repository repository)
        {
            RestorePoint restorePoint = backupJob.CreateRestorePoint();
            List<Storage> storages = repository.MakeVirtualBackUp(algorithm, backupJob.JobObjects, directory.Name, restorePoint);
            foreach (Storage storage in storages)
            {
                restorePoint.Storage.Add(storage);
            }
        }
    }
}