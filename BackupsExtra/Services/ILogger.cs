using System;
using BackupsExtra.Classes;

namespace BackupsExtra.Services
{
    public interface ILogger
    {
        void Loger(DateTime dateTime, string message);
    }
}