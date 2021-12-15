using System;
using System.Collections.Generic;
using Backups.Services;

namespace Backups.Classes
{
    public class Storage
    {
        public Storage()
        {
            Id = Guid.NewGuid();
            ListJobObject = new List<JobObject>();
        }

        public Guid Id { get; }
        public List<JobObject> ListJobObject { get; }

        public void AddJobObject(JobObject jobObject)
        {
            ListJobObject.Add(jobObject);
        }

        public void RemoveJobObject(JobObject jobObject)
        {
            ListJobObject.Remove(jobObject);
        }
    }
}