using System.Collections.Generic;

namespace IsuExtra.Classes
{
    public class ScheduleGroupRepository
    {
        private List<ScheduleGroup> _scheduleGroups = new List<ScheduleGroup>();

        public List<ScheduleGroup> AddSchedulGroupList(ScheduleGroup scheduleGroup)
        {
            _scheduleGroups.Add(scheduleGroup);
            return _scheduleGroups;
        }

        public List<ScheduleGroup> RemoveSchedulGroupList(ScheduleGroup scheduleGroup)
        {
            _scheduleGroups.Remove(scheduleGroup);
            return _scheduleGroups;
        }

        public List<ScheduleGroup> GetSchedulGroupList()
        {
            return _scheduleGroups;
        }
    }
}