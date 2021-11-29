using System.Collections.Generic;
using Backups.Services;

namespace Backups.Classes
{
    public class Split : IAlgorithm
    {
        public List<Storage> MakeStorages(List<JobObject> jobObjects)
        {
            List<Storage> storages = new List<Storage>();
            foreach (JobObject jobObject in jobObjects)
            {
                Storage storage = new Storage();
                storage.AddJobObject(jobObject);
                storages.Add(storage);
            }

            return storages;
        }
    }
}