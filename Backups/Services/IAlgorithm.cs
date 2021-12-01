using System.Collections.Generic;
using Backups.Classes;

namespace Backups.Services
{
    public interface IAlgorithm
    {
        List<Storage> MakeStorages(List<JobObject> jobObjects);
    }
}