using System.Collections.Generic;
using System.Linq;
using Backups.Classes;
using Backups.Services;

namespace BackupsExtra.Classes
{
    public class MergeRestorePoints : BackupService
    {
        public void DeleteDuplicateJobObjects(BackupJob backupJob)
        {
            RestorePoint lastRestorePoint = backupJob.RestorePoints.Last();
            List<Storage> lastStorages = lastRestorePoint.Storages;
            List<JobObject> lastJobObjects = new List<JobObject>();
            foreach (var storage in lastStorages)
            {
                lastJobObjects.AddRange(storage.ListJobObject);
            }

            foreach (var restorePoint in backupJob.RestorePoints)
            {
                if (restorePoint != lastRestorePoint)
                {
                    foreach (var storage in restorePoint.Storages)
                    {
                        foreach (var jobObject in storage.ListJobObject)
                        {
                            foreach (var lastJobObject in lastJobObjects)
                            {
                                if (jobObject == lastJobObject)
                                {
                                    storage.ListJobObject.Remove(jobObject);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void UpdateMissingJobObjects(BackupJob backupJob)
        {
            int i = backupJob.RestorePoints.Count;
            RestorePoint lastrestorePoint = backupJob.RestorePoints[i - 2];
            List<Storage> storages = lastrestorePoint.Storages;
            backupJob.RestorePoints.Last().Storages.AddRange(storages);
            backupJob.RestorePoints.Last().Storages.Remove(backupJob.RestorePoints.Last().Storages.Last());
        }

        public void DeleteSingleStoragePoint(BackupJob backupJob)
        {
            foreach (var restorePoints in backupJob.RestorePoints)
            {
                if (restorePoints.Storages.Count == 1)
                {
                    backupJob.RestorePoints.Remove(restorePoints);
                }
            }
        }
    }
}