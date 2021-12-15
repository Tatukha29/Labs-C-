using System;
using System.Collections.Generic;
using Backups.Classes;

namespace BackupsExtra.Services
{
    public class BackupExtraService
    {
        public List<RestorePoint> DeleteRestorePoints(IDeleteAlgorithm algorithm, BackupJob backupJob, int count, DateTime? dateTime)
        {
            List<RestorePoint> restorePoints = algorithm.DeleteAlgorithm(backupJob, count, dateTime);
            return restorePoints;
        }

        public void Logger(ILogger logger, DateTime dateTime, string message)
        {
            logger.Loger(dateTime, message);
        }
    }
}