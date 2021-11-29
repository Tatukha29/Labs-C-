using System.IO;

namespace Backups.Classes
{
    public class JobObject
    {
        public JobObject(FileInfo path)
        {
            Path = path;
        }

        public FileInfo Path { get; }

        public string FullPath()
        {
            return $@"{Path.DirectoryName}/{Path.Name}";
        }
    }
}