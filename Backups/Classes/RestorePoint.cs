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
            Storage = new List<Storage>();
        }

        public Guid Id { get; }
        public DateTime Data { get; }
        public List<Storage> Storage { get; }
    }
}