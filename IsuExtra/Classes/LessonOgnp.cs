using System;

namespace IsuExtra.Classes
{
    public class LessonOgnp
    {
        public LessonOgnp(DateTime time, DayOfWeek dayOfWeek, string teacher, int room)
        {
            Time = time;
            DayOfWeek = dayOfWeek;
            Teacher = teacher;
            Room = room;
        }

        public DateTime Time { get; }
        public DayOfWeek DayOfWeek { get; }
        public string Teacher { get; }
        public int Room { get; }
    }
}