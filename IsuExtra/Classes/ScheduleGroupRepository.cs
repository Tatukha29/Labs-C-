using System.Collections.Generic;

namespace IsuExtra.Classes
{
    public class ScheduleGroupRepository
    {
        private List<ScheduleGroup> _scheduleGroups = new List<ScheduleGroup>();

        public ScheduleGroup AddSchedulGroup(ScheduleGroup scheduleGroup)
        {
            _scheduleGroups.Add(scheduleGroup);
            return scheduleGroup;
        }

        public void RemoveSchedulGroup(ScheduleGroup scheduleGroup)
        {
            _scheduleGroups.Remove(scheduleGroup);
        }

        public List<ScheduleGroup> GetSchedulGroupList()
        {
            List<ScheduleGroup> newScheduleGroups = new List<ScheduleGroup>();
            newScheduleGroups.AddRange(_scheduleGroups);
            return newScheduleGroups;
        }
    }
}