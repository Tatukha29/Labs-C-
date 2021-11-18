using System;

namespace IsuExtra.Classes
{
    public class LessonOgnp
    {
        public LessonOgnp(string ognpGroup, DateTime time, string teahcer, int room)
        {
            Ognp = ognpGroup;
            Time = time;
            Teacher = teahcer;
            Room = room;
        }

        public string Ognp { get; }
        public DateTime Time { get; }
        public string Teacher { get; }
        public int Room { get; }
    }
}