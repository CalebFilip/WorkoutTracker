using System.Text;
using WorkoutTracker.models;

string projectRoot = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, @"..\..\..\"));
string directoryPath = Path.Combine(projectRoot, "files");


while (true)
{
    Console.WriteLine("1. View Exercise Types");
    Console.WriteLine("2. Create Exercise Type");
    Console.WriteLine("3. Log Exercise");
    Console.WriteLine("4. View Progress");
    Console.WriteLine("5. Exit");
    string choice = Console.ReadLine() ?? "-1";

    switch (choice)
    {
        case "1": ViewExercises(); break;
        case "2": CreateExercise(); break;
        case "3": LogWorkout(); break;
        case "4": ViewProgress(); break;
        case "5": return;
        default: Console.WriteLine("Invalid choice."); break;
    }
}


//CASE 1
void ViewExercises()
{
    Console.WriteLine("\n\nExercises:");
    displayExercisesCSV();
    Console.WriteLine("\n\nPress any key to continue.");
    Console.ReadKey();
    Console.WriteLine("\n\n");
    return;
}
//CASE 1 COMPLETE


//CASE 2
void CreateExercise()
{
    Console.WriteLine("\n\nEnter the name of the exercise:");
    string name = Console.ReadLine() ?? "Unnamed Exercise";

    Console.WriteLine("1: Traditional Weight Exercise \n2: Cardio Exercise");
    if (!int.TryParse(Console.ReadLine(), out int type))
    {
        Console.WriteLine("Invalid input for muscle group.");
        return;
    }

    Exercise exercise = new Exercise(name, type);

    string csvLine = exercise.toCSVLine();

    string filePath = Path.Combine(directoryPath, "exercises.csv");
    File.AppendAllText(filePath, csvLine + Environment.NewLine);

    Console.WriteLine("Exercise created and saved to file.\n\n");
}
//CASE 2 COMPLETE


//CASE 3
void LogWorkout()
{
    Console.WriteLine("\n\nSelect Exercise Type: ");
    displayExercisesCSV();
    if (!int.TryParse(Console.ReadLine(), out int exerciseLine))
    {
        Console.WriteLine("Invalid input.");
        return;
    }

    string filePath = Path.Combine(directoryPath, "exercises.csv");
    string[] lines = File.ReadAllLines(filePath);
    if (exerciseLine < 1 || exerciseLine > lines.Length)
    {
        Console.WriteLine("Invalid exercise selection.");
        return;
    }

    string selectedExercise = lines[exerciseLine - 1];
    string[] exerciseDecider = selectedExercise.Split(',');
    int result;
    if (int.Parse(exerciseDecider[1]) == 1) result = CreateTraditional(exerciseDecider[0]);
    else result = CreateCardio(exerciseDecider[0]);
    if (result != 0) { Console.WriteLine("Error With Logging Workout"); return; }
    Console.WriteLine("Exercise Logged!!\n\n");
    return;

}


//public CardioExerciseLog(string name, int minutes, int sets, DateOnly date) : base(name, sets, date)
int CreateCardio(string name)
{
    //sets and minutes need user input. date is current date.
    Console.WriteLine("\nHow many sets?");
    if (!int.TryParse(Console.ReadLine(), out int sets))
    {
        Console.WriteLine("Invalid input.");
        return 1;
    }

    Console.WriteLine("How long was each set in whole minutes?");
    if (!int.TryParse(Console.ReadLine(), out int minutes))
    {
        Console.WriteLine("Invalid input.");
        return 1;
    }

    DateOnly date = DateOnly.FromDateTime(DateTime.Now);

    CardioExerciseLog cardioExercise = new CardioExerciseLog(name, minutes, sets, date);

    persistLog(cardioExercise.toCSVLine());

    return 0;

}

//public StrengthExerciseLog(string name, double weight, int reps, int sets, DateOnly date) : base(name, sets, date)
int CreateTraditional(string name)
{
    //weight, reps, sets need user input. date is current date.
    Console.WriteLine("\nHow many sets?");
    if (!int.TryParse(Console.ReadLine(), out int sets))
    {
        Console.WriteLine("Invalid input.");
        return 1;
    }

    Console.WriteLine("How many reps per set?");
    if (!int.TryParse(Console.ReadLine(), out int reps))
    {
        Console.WriteLine("Invalid input.");
        return 1;
    }

    Console.WriteLine("How much weight in lbs?");
    if (!float.TryParse(Console.ReadLine(), out float weight))
    {
        Console.WriteLine("Invalid input.");
        return 1;
    }

    DateOnly date = DateOnly.FromDateTime(DateTime.Now);

    StrengthExerciseLog strengthExercise = new StrengthExerciseLog(name, weight, reps, sets, date);

    persistLog(strengthExercise.toCSVLine());

    return 0;

}

void persistLog(string log)
{
    string filePath = Path.Combine(directoryPath, "log.csv");
    File.AppendAllText(filePath, log + Environment.NewLine);
}
//CASE 3 COMPLETE



//CASE 4
void ViewProgress()
{
    Console.WriteLine("\n\n1: Display By Workout\n2: Display By Date");
    string choice = Console.ReadLine() ?? "-1";

    switch (choice)
    {
        case "2": DisplayLogsByDate(); break;
        case "1": DisplayLogsByExercise(); break;
        default: Console.WriteLine("Invalid choice."); break;
    }
}

void DisplayLogsByDate()
{
    string filePath = Path.Combine(directoryPath, "log.csv");
    string[] lines = File.ReadAllLines(filePath);
    DateOnly currentDate = DateOnly.Parse("01/01/1000");
    foreach (string line in lines.Reverse())
    {
        string[] log = line.Split(',');
        if (DateOnly.Parse(log[0]) != currentDate)
        {
            currentDate = DateOnly.Parse(log[0]);
            Console.WriteLine("\n" + currentDate);
        }
        if (log.Length == 5) printStrengthExerciseForDate(log);
        if (log.Length == 4) printCardioExerciseForDate(log);
    }
    Console.WriteLine("\n");
}

void printStrengthExerciseForDate(string[] log)
{
    //ex: 5/5/2025,Bench Press,4,10,185
    Console.WriteLine($"{log[1]}: {log[2]}x{log[3]} at {log[4]} lbs.");
}

void printCardioExerciseForDate(string[] log)
{
    //ex: 5/5/2025,Run,1,24
    Console.WriteLine($"{log[1]} for {log[2]} set(s) of {log[3]} minutes.");
}


void DisplayLogsByExercise()
{
    Console.WriteLine("Choose an Exercise:");
    displayExercisesCSV();
    if (!int.TryParse(Console.ReadLine(), out int exerciseLine))
    {
        Console.WriteLine("Invalid input.");
        return;
    }

    string filePath1 = Path.Combine(directoryPath, "exercises.csv");
    string[] lines1 = File.ReadAllLines(filePath1);
    if (exerciseLine < 1 || exerciseLine > lines1.Length)
    {
        Console.WriteLine("Invalid exercise selection.");
        return;
    }

    string selectedExercise = lines1[exerciseLine - 1];
    string exerciseName = selectedExercise.Split(",")[0];
    Console.WriteLine("\n\n" + exerciseName);


    string filePath = Path.Combine(directoryPath, "log.csv");
    string[] lines = File.ReadAllLines(filePath);
    foreach (string line in lines.Reverse())
    {
        string[] log = line.Split(',');
        if (log[1] == exerciseName)
        {
            if (log.Length == 5) printStrengthExercise(log);
            if (log.Length == 4) printCardioExercise(log);
        }
    }
    Console.WriteLine("\n");
}

void printStrengthExercise(string[] log)
{
    //ex: 5/5/2025,Bench Press,4,10,185
    Console.WriteLine($"{log[0]}: {log[1]} {log[2]}x{log[3]} at {log[4]} lbs.");
}

void printCardioExercise(string[] log)
{
    //ex: 5/5/2025,Run,1,24
    Console.WriteLine($"{log[0]}: {log[1]} for {log[2]} set(s) of {log[3]} minutes.");
}

//CASE 4 COMPLETE


void displayExercisesCSV()
{
    string filePath = Path.Combine(directoryPath, "exercises.csv");
    string[] lines = File.ReadAllLines(filePath);
    int i = 1;
    foreach (string line in lines)
    {
        String[] exercise = line.Split(',');
        if (int.Parse(exercise[1]) == 2) exercise[1] = "Cardio Exercise";
        else exercise[1] = "Traditional Weight Exercise";
        Console.WriteLine($"{i}: {exercise[0]}, {exercise[1]}");
        i++;

    }
}
