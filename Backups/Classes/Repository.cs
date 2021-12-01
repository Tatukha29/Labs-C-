using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using Backups.Services;
using Ionic.Zip;

namespace Backups.Classes
{
    public class Repository
    {
        public Repository(DirectoryInfo directory)
        {
            Directory = directory;
        }

        public DirectoryInfo Directory { get; }
        public void BackUp(IRepository backup, IAlgorithm algorithm, List<JobObject> jobObjects, RestorePoint restorePoint)
        {
            List<Storage> storages = backup.MakeBackup(algorithm, jobObjects, restorePoint, Directory);
        }

        /*public List<Storage> MakeVirtualBackUp(IAlgorithm algorithm, List<JobObject> jobObjects, string nameDirectory, RestorePoint restorePoint)
        {
            List<Storage> storages = algorithm.MakeStorages(jobObjects);
            string path;
            foreach (Storage storage in storages)
            {
                foreach (JobObject jobObject in storage.ListJobObject.ToList())
                {
                    path = @$"{Directory.FullName}/{nameDirectory}/{jobObject.Path.Name}{restorePoint.Id}.zip";
                    JobObject newJobObject = new JobObject(new FileInfo(path));
                    storage.AddJobObject(newJobObject);
                }
            }

            return storages;
        }

        public List<Storage> MakeLocalBackUp(IAlgorithm algorithm, List<JobObject> jobObjects, string nameDirectory, RestorePoint restorePoint)
        {
            List<Storage> storages = algorithm.MakeStorages(jobObjects);
            string path;
            foreach (Storage storage in storages)
            {
                var zip = new ZipFile();
                foreach (JobObject jobObject in storage.ListJobObject.ToList())
                {
                    zip.AddFile(jobObject.FullPath(), "/");
                    path = @$"{Directory.FullName}/{nameDirectory}/{jobObject.Path.Name}{restorePoint.Id}.zip";
                    JobObject newJobObject = new JobObject(new FileInfo(path));
                    storage.AddJobObject(newJobObject);
                }

                zip.Save($@"{Directory.FullName}/BackUp{storage.Id}.zip");
            }

            return storages;
        }*/
    }
}