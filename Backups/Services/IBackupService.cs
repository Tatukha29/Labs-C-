using System.Collections.Generic;
using System.IO;
using Backups.Classes;

namespace Backups.Services
{
    public interface IBackupService
    {
        DirectoryInfo CreateDirectory(string path);
        BackupJob CreateBackupJob();
        Repository CreateRepository(DirectoryInfo directory);
        void StartLocalBackup(IAlgorithm algorithm, List<JobObject> jobObjects, DirectoryInfo directory, BackupJob backupJob, Repository repository);
        void StartVirtualBackup(IAlgorithm algorithm, List<JobObject> jobObjects, DirectoryInfo directory, BackupJob backupJob, Repository repository);
    }
}