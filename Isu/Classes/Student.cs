using System;
using Isu.Services;

namespace Isu.Classes
{
    public class Student
    {
        public Student(string name, Group group)
        {
            Name = name;
            Id = new StudentId().Id;
            Group = group;
        }

        public string Name { get; }
        public int Id { get; }
        public Group Group { get; set; }
    }
}