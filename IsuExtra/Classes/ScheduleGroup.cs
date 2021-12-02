using System;
using Isu.Classes;

namespace IsuExtra.Classes
{
    public class ScheduleGroup
    {
        public ScheduleGroup(Group group, DateTime time, DayOfWeek dayOfWeek, string teacher, int room)
        {
            Group = group;
            Time = time;
            DayOfWeek = dayOfWeek;
            Teacher = teacher;
            Room = room;
        }

        public Group Group { get; }
        public DateTime Time { get; }
        public DayOfWeek DayOfWeek { get; }
        public string Teacher { get; }
        public int Room { get; }
    }
}