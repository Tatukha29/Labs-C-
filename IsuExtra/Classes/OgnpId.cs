namespace IsuExtra
{
    public class OgnpId
    {
        private static int _ognpId;

        public OgnpId()
        {
            Id = ++_ognpId;
        }

        public int Id { get; }
    }
}