# Workout Tracker Program

The Workout Tracker Program allows a user to log their workouts and view their progress.

## Start

To Start with the program a user should first create some exercise types such as
`Bench Press`, `Treadmill Jog`, `Dumbell Incline Chest Press`
There is some default entries as examples located in /file/exercises.csv

## Logging

To log an exercise the user will be presented with all of the created exercise types. After choosing the exercise to log the user will input information about their exercise such as sets, reps, weight, and time.
Like the exercise types, there are some default entries as examples located in /files/log.csv
## View Progress

The user can view their progress in two ways. -**By Date**: This will show workouts grouped by dates ordered chronologically descending. -**By Exercise**: This will show the user's progress through one specified exercise ordered chronologically descending.

## Instructions

- **Step 1**: Install the .NET SDK.

  - Verify installation by running:

    `dotnet  --version`

    Ensure the version is 9.0 or higher.

- **Step 2**: Open the project directory. Navigate to the project folder:
- **Step 3**: Build the project. Run the following command to compile the project:

  `dotnet  build`

- **Step 4**: Run the program. Execute the program using:

  `dotnet  run`

**Files Required for Execution**

- Ensure the following files are present in the files directory:
  - exercises.csv
  - log.csv
