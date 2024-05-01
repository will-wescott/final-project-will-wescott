//Will W
//Final Project
// 4/30/24
using System.Diagnostics;

Stopwatch restStopwatch = new Stopwatch();

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


static string GetSkillLevel()
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
            foreach (string item in inputs)
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
                {
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

static List<string> BuildWorkout(List<string> muscleGroups, string skillLevel)
{
    List<string> workoutPlan = new List<string>();
    Random rng = new Random();
    foreach (var muscleGroup in muscleGroups)
    {
        string filename = $"{skillLevel}Workouts{muscleGroup}.txt";
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
        catch (IOException)
        {
            Console.WriteLine($"Error: Could not read file for {muscleGroup}. Make sure the file exists.");
        }
    }
    return workoutPlan;
}
