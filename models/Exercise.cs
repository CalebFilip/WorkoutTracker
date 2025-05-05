namespace WorkoutTracker.models
{

    public class Exercise
    {
        public string Name { get; set; }
        public int Type { get; set; }

        public Exercise(string name, int type)
        {
            Name = name;
            Type = type;
        }

        public string toCSVLine()
        {
            return $"{Name},{Type}";
        }
    }
}