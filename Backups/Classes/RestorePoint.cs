using System;
using System.Collections.Generic;

namespace Backups.Classes
{
    public class RestorePoint
    {
        public RestorePoint()
        {
            Id = Guid.NewGuid();
            Data = DateTime.Now;
            Storages = new List<Storage>();
        }

        public Guid Id { get; }
        public DateTime Data { get; }
        public List<Storage> Storages { get; }
    }
}