using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Classes;
using BackupsExtra.Services;

namespace BackupsExtra.Classes
{
    public class HybridOneAlgorithm : IDeleteAlgorithm
    {
        public List<RestorePoint> DeleteAlgorithm(BackupJob backupJob, int count, DateTime? dateTime)
        {
            var norma = 0;
            List<RestorePoint> trash = backupJob.RestorePoints;
            List<RestorePoint> list = new List<RestorePoint>();
            RestorePoint point = backupJob.RestorePoints.Last();
            foreach (var restorePoint in trash)
            {
                if (restorePoint.Data < point.Data)
                {
                    list.Add(restorePoint);
                    norma++;
                    if (norma == count)
                    {
                        break;
                    }
                }
            }

            foreach (var restorePoint in list)
            {
                backupJob.RestorePoints.Remove(restorePoint);
            }

            if (backupJob.RestorePoints.Count == 0)
            {
                throw new Exception("You can't remove all restorePoints");
            }

            return backupJob.RestorePoints;
        }
    }
}