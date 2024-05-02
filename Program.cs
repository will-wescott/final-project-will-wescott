//Will W
//Final Project
// 4/30/24
using System.Diagnostics;
// Simple test
Debug.Assert(BuildWorkout(new List<string> { "Chest" }, "Advanced").Count > 0, "BuildWorkout failed to create workout plan.");

Stopwatch restStopwatch = new Stopwatch();
//user menu
Console.WriteLine("Welcome to the Workout Builder!");
Console.WriteLine("Select up to three muscle groups for your workout:");
Console.WriteLine("1. Chest");
Console.WriteLine("2. Back");
Console.WriteLine("3. Legs");
Console.WriteLine("4. Shoulders");
Console.WriteLine("5. Arms");
Console.WriteLine("6. Abs");
Console.WriteLine("7. Input Custom Workout");
List<string> selectedMuscleGroups = GetUserInput();
string skillLevel = GetSkillLevel();
List<string> workoutPlan = BuildWorkout(selectedMuscleGroups, skillLevel);

foreach (var exercise in workoutPlan)
{
    Console.WriteLine(exercise);
    StartRestTimer();
    WaitForFinishWorkout();
    CalculateAndDisplayAverageRestTime(); 
}
void StartRestTimer()
    {
        Console.WriteLine("Press any key to start rest time...");
        Console.ReadKey();
        Console.WriteLine("Rest time started. Press 's' to stop.");
        restStopwatch.Restart();
        while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.S)
        {
            // Wait for 's' key to stop
        }
        restStopwatch.Stop();
        Console.WriteLine("Rest time stopped.");
    }

    void WaitForFinishWorkout()
    {
        Console.WriteLine("Press 'q' to finish your workout...");
        while (!Console.KeyAvailable || Console.ReadKey(true).Key != ConsoleKey.Q)
        {
            // Wait for 'q' key to be pressed
        }
    }

    void CalculateAndDisplayAverageRestTime()
    {
        double averageRestTimeInSeconds = restStopwatch.Elapsed.TotalSeconds;
        Console.WriteLine($"Average rest time: {averageRestTimeInSeconds} seconds.");
    } 

static string GetSkillLevel() //handles skill level input
{
    Console.WriteLine("Enter your skill level (Beginner/Advanced):");
    string level = Console.ReadLine();
    return level.Trim();
}

static List<string> GetUserInput()
{
    List<string> selectedMuscleGroups = new List<string>();
    bool inputValidated = false;
    while (!inputValidated)
    {
        Console.WriteLine("Enter the numbers corresponding to the muscle groups you want to use (separated by commas):");
        string input = Console.ReadLine();
        string[] inputs = input.Split(',');
        if (inputs.Length <= 3)
        {
            foreach (string item in inputs) // handles user input
            {
                if (int.TryParse(item, out int muscleGroupIndex) && muscleGroupIndex >= 1 && muscleGroupIndex <= 7)
                {
                    switch (muscleGroupIndex)
                    {
                        case 1:
                            selectedMuscleGroups.Add("Chest");
                            break;
                        case 2:
                            selectedMuscleGroups.Add("Back");
                            break;
                        case 3:
                            selectedMuscleGroups.Add("Legs");
                            break;
                        case 4:
                            selectedMuscleGroups.Add("Shoulders");
                            break;
                        case 5:
                            selectedMuscleGroups.Add("Arms");
                            break;
                        case 6:
                            selectedMuscleGroups.Add("Abs");
                            break;
                        case 7:
                            selectedMuscleGroups.Add("Custom");
                            break;
                    }
                }
                else
                { //prevents entering invalid number
                    Console.WriteLine("Invalid input. Please enter numbers between 1 and 7 separated by commas.");
                    selectedMuscleGroups.Clear();
                    break;
                }
            }
            if (selectedMuscleGroups.Count > 0)
            {
                inputValidated = true;
            }
        }
        else
        {
            Console.WriteLine("You can only select up to three muscle groups. Please try again.");
        }
    }
    return selectedMuscleGroups;
}

List<string> BuildWorkout(List<string> muscleGroups, string skillLevel)
    {
        List<string> workoutPlan = new List<string>();
        Random rng = new Random();
        foreach (var muscleGroup in muscleGroups)
        {
            if (muscleGroup.Equals("Custom"))
            {
                workoutPlan.AddRange(GetCustomWorkout()); // Call the method to get custom workout
            }
            else
            {
                string filename = $"{skillLevel}Workouts{muscleGroup}.txt";// pulls data from files to randomly compile into workout
                try
                {
                    var exercises = File.ReadAllLines(filename);
                    exercises = exercises.OrderBy(x => rng.Next()).Take(rng.Next(3, 6)).ToArray(); // Select 3-5 random exercises
                    foreach (var exercise in exercises)
                    {
                        string repInfo = muscleGroup.Equals("Abs") ? "15-20 reps x 4 sets" : "8-12 reps x 4 sets";
                        workoutPlan.Add($"{exercise} - {repInfo}");
                    }
                }
                catch (IOException) //handles errors if the file doesnt exist
                {
                    Console.WriteLine($"Error: Could not read file for {muscleGroup}. Make sure the file exists.");
                }
            }
        }
        return workoutPlan;
    }

    List<string> GetCustomWorkout() // handles custom workout user input
    {
        Console.WriteLine("Enter your custom workout (press Enter after each exercise, type 'done' to finish):");
        List<string> customWorkout = new List<string>();
        string exercise = Console.ReadLine();
        while (exercise.ToLower() != "done")
        {
            customWorkout.Add(exercise);
            exercise = Console.ReadLine();
        }
        return customWorkout;
    }
