using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Classes;
using BackupsExtra.Services;
using Newtonsoft.Json;

namespace BackupsExtra.Classes
{
    public class OriginalLocation : IRecovery
    {
        public void RecoveryObject(BackupJob backupJob, DirectoryInfo path)
        {
            foreach (var storage in backupJob.RestorePoints.Last().Storages)
            {
                foreach (var jobObject in storage.ListJobObject)
                {
                    if (jobObject.Path.Name != "FileA" && jobObject.Path.Name != "FileB")
                    {
                        ZipFile.ExtractToDirectory(jobObject.FullPath(), path.FullName);
                    }
                }
            }
        }
    }
}