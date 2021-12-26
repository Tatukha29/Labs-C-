using System;
using System.Collections.Generic;
using System.IO;
using Backups.Services;

namespace Backups.Classes
{
    public class BackupJob
    {
        public BackupJob()
        {
            JobObjects = new List<JobObject>();
            RestorePoints = new List<RestorePoint>();
        }

        public List<JobObject> JobObjects { get; }
        public List<RestorePoint> RestorePoints { get; }

        public void AddJobObject(JobObject jobObject)
        {
            JobObjects.Add(jobObject);
        }

        public void RemoveJobObject(JobObject jobObject)
        {
            JobObjects.Remove(jobObject);
        }

        public RestorePoint CreateRestorePoint(DirectoryInfo directory)
        {
            RestorePoint restorePoint = new RestorePoint(directory);
            RestorePoints.Add(restorePoint);
            return restorePoint;
        }

        public void RemoveRestorePoint(RestorePoint restorePoint)
        {
            RestorePoints.Remove(restorePoint);
        }
    }
}