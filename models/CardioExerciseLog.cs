namespace WorkoutTracker.models
{

    public class CardioExerciseLog : ExerciseLog
    {
        public int Minutes { get; set; }
        public CardioExerciseLog(string name, int minutes, int sets, DateOnly date) : base(name, sets, date)
        {
            Minutes = minutes;
        }

        public override string ToString()
        {
            return $"{Name} - {Minutes} minutes, {Sets} sets on {Date}";
        }

        public string toCSVLine()
        {
            return $"{Date},{Name},{Sets},{Minutes}";
        }
    }
}