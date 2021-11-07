using System;
using System.Collections.Generic;
using System.ComponentModel;
using Isu.Classes;

namespace IsuExtra.Classes
{
    public class Lesson
    {
        public Lesson(string name, string time, int day, int size)
        {
            Name = name;
            Time = time;
            Day = day;
            StudentsOgnp = new List<Student>();
            Size = size;
        }

        public string Name { get; }
        public string Time { get; }
        public int Day { get; }
        public int Size { get; set; }
        public List<Student> StudentsOgnp { get; }
    }
}