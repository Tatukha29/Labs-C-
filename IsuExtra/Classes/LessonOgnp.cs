using System;

namespace IsuExtra.Classes
{
    public class LessonOgnp
    {
        public LessonOgnp(string ognpGroupName, DateTime time, int dayOfWeek, string teacher, int room)
        {
            OgnpGroupName = ognpGroupName;
            Time = time;
            DayOfWeek = dayOfWeek;
            Teacher = teacher;
            Room = room;
        }

        public string OgnpGroupName { get; }
        public DateTime Time { get; }
        public int DayOfWeek { get; }
        public string Teacher { get; }
        public int Room { get; }
    }
}