using System.Collections.Generic;

namespace IsuExtra.Classes
{
    public class OgnpRepository
    {
        private List<Ognp> _ognps = new List<Ognp>();

        public List<Ognp> AddOgnpList(Ognp ognp)
        {
            _ognps.Add(ognp);
            return _ognps;
        }

        public List<Ognp> RemoveOgnpList(Ognp ognp)
        {
            _ognps.Remove(ognp);
            return _ognps;
        }

        public List<Ognp> GetOgnpList()
        {
            return _ognps;
        }
    }
}