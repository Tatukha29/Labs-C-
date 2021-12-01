using System.Collections.Generic;
using System.IO;
using Backups.Classes;

namespace Backups.Services
{
    public interface IBackupService
    {
        DirectoryInfo CreateDirectory(string path);
        BackupJob CreateBackupJob();
        void StartBackup(IRepository backup, IAlgorithm algorithm, List<JobObject> jobObjects, DirectoryInfo directory, BackupJob backupJob);
    }
}