using System;
using System.Collections.Generic;
using Backups.Services;

namespace Backups.Classes
{
    public class SingleAlgorithm : IAlgorithm
    {
        public List<Storage> MakeStorages(List<JobObject> jobObjects)
        {
            List<Storage> storages = new List<Storage>();
            Storage storage = new Storage();
            foreach (JobObject jobObject in jobObjects)
            {
                storage.AddJobObject(jobObject);
            }

            storages.Add(storage);
            return storages;
        }
    }
}