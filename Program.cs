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





