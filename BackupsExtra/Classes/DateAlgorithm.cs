using System;
using System.Collections.Generic;
using Backups.Classes;
using BackupsExtra.Services;

namespace BackupExtra.Classes
{
    public class DateAlgorithm : IDeleteAlgorithm
    {
        public List<RestorePoint> DeleteAlgorithm(BackupJob backupJob, int count, DateTime? dateTime)
        {
            foreach (var restorePoint in backupJob.RestorePoints)
            {
                if (restorePoint.Data < dateTime)
                {
                    backupJob.RestorePoints.Remove(restorePoint);
                }
            }

            if (backupJob.RestorePoints.Count == 0)
            {
                throw new Exception("You can't remove all restorePoints");
            }

            return backupJob.RestorePoints;
        }
    }
}