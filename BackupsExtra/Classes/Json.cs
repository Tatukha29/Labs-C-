using System.IO;
using Backups.Classes;
using Backups.Services;
using Newtonsoft.Json;

namespace BackupsExtra.Classes
{
    public class Json : BackupService
    {
        public void Cerialize(BackupJob backupJob)
        {
            string json = JsonConvert.SerializeObject(backupJob, Formatting.Indented, new Conversion());
            File.AppendAllText("tanya.json", json);
        }

        public BackupJob Decerialize(string path)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Objects,
            };
            string json = File.ReadAllText(path);
            BackupJob backupJob = JsonConvert.DeserializeObject<BackupJob>(json, new Conversion());
            return backupJob;
        }
    }
}