namespace WorkoutTracker.models
{

    public abstract class ExerciseLog
    {
        public string Name { get; set; }
        public int Sets { get; set; }

        public DateOnly Date { get; set; }

        public ExerciseLog(string name, int sets, DateOnly date)
        {
            Name = name;
            Sets = sets;
            Date = date;
        }
    }
}