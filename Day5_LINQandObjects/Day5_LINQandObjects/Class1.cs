using System;

namespace LINQandObjects
{
    public class Person
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Hometown { get; set; }

        public override string ToString()
        {
            return $"{Name}, {Gender}, {Age}, {Hometown}";
        }
    }
}
