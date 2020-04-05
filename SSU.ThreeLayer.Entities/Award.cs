using System;

namespace SSU.ThreeLayer.Entities
{
    public class Award
    {
        public uint Id { get; set; }
        public string Title { get; set; }

        public Award(string title)
        {
            Title = title;
        }

        public Award(uint id, string title)
        {
            Id = id;
            Title = title;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public string GetStringToShow()
        {
            return "Award № " + Id + Environment.NewLine + "   Title: " + Title;
        }
    }
}
