using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using Backups.Services;
using Ionic.Zip;
namespace Backups.Classes
{
    public class Virtual : IRepository
    {
        public List<Storage> MakeBackup(IAlgorithm algorithm, List<JobObject> jobObjects, RestorePoint restorePoint, DirectoryInfo directory)
        {
            List<Storage> storages = algorithm.MakeStorages(jobObjects);
            string path;
            foreach (Storage storage in storages)
            {
                foreach (JobObject jobObject in storage.ListJobObject.ToList())
                {
                    path = @$"{directory.FullName}/{directory.Name}/{jobObject.Path.Name}{restorePoint.Id}.zip";
                    JobObject newJobObject = new JobObject(new FileInfo(path));
                    storage.AddJobObject(newJobObject);
                }
            }

            return storages;
        }
    }
}