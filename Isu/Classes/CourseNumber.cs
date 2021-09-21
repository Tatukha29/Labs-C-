using Isu.Tools;

namespace Isu.Classes
{
    public class CourseNumber
    {
        public CourseNumber(int course)
        {
            if (course >= 1 && course <= 4)
            {
                Course = course;
            }
            else
            {
                throw new IsuException("Invalid course number");
            }
        }

        public int Course { get; }
    }
}