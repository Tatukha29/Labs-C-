using System;
using System.Collections.Generic;
using Backups.Classes;

namespace BackupsExtra.Services
{
    public interface IDeleteAlgorithm
    {
        List<RestorePoint> DeleteAlgorithm(BackupJob backup, int count, DateTime? dateTime = null);
    }
}