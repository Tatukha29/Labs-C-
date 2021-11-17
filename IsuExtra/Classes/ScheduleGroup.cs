using System;
using Isu.Classes;

namespace IsuExtra.Classes
{
    public class ScheduleGroup
    {
        public ScheduleGroup(Group group, DateTime time, string teacher, int room)
        {
            Group = group;
            Time = time;
            Teacher = teacher;
            Room = room;
        }

        public Group Group { get; }
        public DateTime Time { get; }
        public string Teacher { get; }
        public int Room { get; }
    }
}