using System;
using Isu.Classes;

namespace IsuExtra.Classes
{
    public class ScheduleGroup
    {
        public ScheduleGroup(Group group,  string time, int day)
        {
            Group = group;
            Time = time;
            Day = day;
        }

        public Group Group { get; }
        public string Time { get; }
        public int Day { get; }
    }
}