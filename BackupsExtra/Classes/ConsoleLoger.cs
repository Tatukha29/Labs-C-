using System;
using BackupsExtra.Services;

namespace BackupsExtra.Classes
{
    public class ConsoleLoger : ILogger
    {
        public void Loger(DateTime dateTime, string message)
        {
            Console.WriteLine(dateTime + message);
        }
    }
}