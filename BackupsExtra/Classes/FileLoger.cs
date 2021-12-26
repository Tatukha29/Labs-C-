using System;
using System.IO;
using System.Net;
using BackupsExtra.Services;

namespace BackupsExtra.Classes
{
    public class FileLoger : ILogger
    {
        public void Loger(DateTime dateTime, string message)
        {
            File.AppendAllText("FileLogger.txt", message);
        }
    }
}