using System.Collections.Generic;

namespace IsuExtra.Classes
{
    public class OgnpRepository
    {
        private List<Ognp> _ognps = new List<Ognp>();

        public Ognp AddOgnp(Ognp ognp)
        {
            _ognps.Add(ognp);
            return ognp;
        }

        public void RemoveOgnp(Ognp ognp)
        {
            _ognps.Remove(ognp);
        }

        public List<Ognp> GetAll()
        {
            return _ognps;
        }
    }
}