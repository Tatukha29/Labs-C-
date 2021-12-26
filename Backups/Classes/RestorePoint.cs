using System;
using System.Collections.Generic;
using System.IO;

namespace Backups.Classes
{
    public class RestorePoint
    {
        public RestorePoint(DirectoryInfo path)
        {
            Id = Guid.NewGuid();
            Data = DateTime.Now;
            Directory = path;
            Storages = new List<Storage>();
        }

        public Guid Id { get; }
        public DateTime Data { get; }
        public DirectoryInfo Directory { get; }
        public List<Storage> Storages { get; }
    }
}