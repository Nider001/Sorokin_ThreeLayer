using System;

namespace SSU.ThreeLayer.Entities
{
    public class User
    {
        public uint Id { get; set; } = 0;
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - DateOfBirth.Year;
                if (DateOfBirth.Date > today.AddYears(-age))
                {
                    age--;
                }

                return age;
            }
        }

        public User(string name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public User(uint id, string name, DateTime dateOfBirth)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public string GetStringToShow()
        {
            return "User № " + Id + Environment.NewLine + "   Name: " + Name + ":" + Environment.NewLine + "   Date of birth: " + Convert.ToString(DateOfBirth) + ":" + Environment.NewLine + "   Age: " + Age;
        }
    }
}
