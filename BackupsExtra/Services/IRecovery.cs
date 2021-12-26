using System.IO;
using Backups.Classes;

namespace BackupsExtra.Services
{
    public interface IRecovery
    {
        void RecoveryObject(BackupJob backupJob, DirectoryInfo path);
    }
}