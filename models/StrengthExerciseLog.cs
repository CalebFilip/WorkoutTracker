namespace WorkoutTracker.models
{
    public class StrengthExerciseLog : ExerciseLog
    {
        public double Weight { get; set; }
        public int Reps { get; set; }

        public StrengthExerciseLog(string name, double weight, int reps, int sets, DateOnly date) : base(name, sets, date)
        {
            Weight = weight;
            Reps = reps;
        }

        public override string ToString()
        {
            return $"{Name} - {Weight} lbs, {Reps} reps, {Sets} sets";
        }

        public string toCSVLine()
        {
            return $"{Date},{Name},{Sets},{Reps},{Weight}";
        }
    }
}