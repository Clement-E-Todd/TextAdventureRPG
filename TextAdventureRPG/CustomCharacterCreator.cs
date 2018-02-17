using System;

static class CustomCharacterCreator
{
	public static PlayerCharacter CreatePlayerCharacter()
	{
		// Prompt the use to enter a name for the new character
		string name = PromptForName();

		// Prompt the user to specify the character's pronouns
		Pronouns pronouns = PromptForPronouns();

		// Prompt for the character's strongest and weakest traits, which will determine their stats
		Character.Stats statIncreasePerLevel = PromptForStats();

		// Create and return the new character using the information specified by the user
		PlayerCharacter character = new PlayerCharacter(name, pronouns, statIncreasePerLevel);
		return character;
	}

	private static string PromptForName()
	{
		Console.Clear();
		Console.WriteLine("What is your character's name?");
		string name = "";

		while (name == "")
		{
			name = Console.ReadLine();
		}

		return name;
	}

	private static Pronouns PromptForPronouns()
	{
		Console.Clear();
		Console.WriteLine("What is your character's gender?");
		Console.WriteLine("[F] Female");
		Console.WriteLine("[M] Male");
		Console.WriteLine("[O] Other (Specify)");
		Pronouns pronouns = null;

		while (pronouns == null)
		{
			string userInput = Console.ReadLine().ToUpper();

			if (userInput == "F")
			{
				pronouns = Pronouns.Presets.feminine;
			}
			else if (userInput == "M")
			{
				pronouns = Pronouns.Presets.masculine;
			}
			else if (userInput == "O")
			{
				pronouns = PromptForNonbinaryPronouns();
			}
			else
			{
				Console.WriteLine("Invalid input.");
			}
		}

		return pronouns;
	}

	private static Pronouns PromptForNonbinaryPronouns()
	{
		Console.Clear();
		Console.WriteLine("What are your character's pronouns?");
		for (int i = 0; i < Pronouns.Presets.nonbinary.Length; i++)
		{
			Console.WriteLine("[" + (i + 1) + "] " + Pronouns.Presets.nonbinary[i].they + " / " + Pronouns.Presets.nonbinary[i].them);
		}
		Console.WriteLine("[C] Custom");
		Pronouns gender = null;

		while (gender == null)
		{
			string userInput = Console.ReadLine().ToUpper();

			int numericInput;
			bool isInputNumeric = int.TryParse(userInput, out numericInput);

			if (isInputNumeric && numericInput >= 1 && numericInput <= Pronouns.Presets.nonbinary.Length)
			{
				gender = Pronouns.Presets.nonbinary[numericInput - 1];
			}
			else if (userInput == "C")
			{
				gender = Pronouns.CreateCustom();
			}
			else
			{
				Console.WriteLine("Invalid input.");
			}
		}

		return gender;
	}

	private static Character.Stats PromptForStats()
	{
		// Prompt the user to specify the character's strongest trait
		Console.Clear();
		Console.WriteLine("What is your character's strongest trait?");
		Console.WriteLine("[S] Strength and Vitality");
		Console.WriteLine("[M] Magical Prowess");
		Console.WriteLine("[A] Agility and Dexterity");
		string strongestTrait = null;

		while (strongestTrait == null)
		{
			string userInput = Console.ReadLine().ToUpper();

			if (userInput == "S" || userInput == "M" || userInput == "A")
			{
				strongestTrait = userInput;
			}
			else
			{
				Console.WriteLine("Invalid input.");
			}
		}

		// Prompt the user to specify the character's weakest trait
		Console.Clear();
		Console.WriteLine("What is your character's weakest trait?");

		if (strongestTrait != "S")
		{
			Console.WriteLine("[S] Strength and Vitality");
		}
		if (strongestTrait != "M")
		{
			Console.WriteLine("[M] Magical Prowess");
		}
		if (strongestTrait != "A")
		{
			Console.WriteLine("[A] Agility and Dexterity");
		}
		string weakestTrait = null;

		while (weakestTrait == null)
		{
			string userInput = Console.ReadLine().ToUpper();

			if (userInput == "S" || userInput == "M" || userInput == "A")
			{
				if (userInput != strongestTrait)
				{
					weakestTrait = userInput;
				}
				else
				{
					Console.WriteLine("The strongest and weakest trait can't be the same!");
				}
			}
			else
			{
				Console.WriteLine("Invalid input.");
			}
		}

		// Determine how much each of the character's traits will increase per level based on
		// the strongest and weakest traits specified by the user
		Character.Stats statIncreasePerLevel = new Character.Stats(10, 10, 3, 3, 3);

		// Boost whichever traits were chosen to be the strongest...
		if (strongestTrait == "S")
		{
			// Boost physical traits...
			statIncreasePerLevel.hp += 3;
			statIncreasePerLevel.strength += 2;
		}
		else if (strongestTrait == "M")
		{
			// Boost magical traits...
			statIncreasePerLevel.sp += 3;
			statIncreasePerLevel.magic += 2;
		}
		else if (strongestTrait == "A")
		{
			// Boost speed trait...
			statIncreasePerLevel.speed += 2;
		}

		// Reduce whichever traits were chosen to be the weakest...
		if (weakestTrait == "S")
		{
			// Reduce physical traits...
			statIncreasePerLevel.hp -= 2;
			statIncreasePerLevel.strength -= 1;
		}
		else if (weakestTrait == "M")
		{
			// Reduce magical traits...
			statIncreasePerLevel.sp -= 2;
			statIncreasePerLevel.magic -= 1;
		}
		else if (weakestTrait == "A")
		{
			// Reduce speed trait...
			statIncreasePerLevel.speed -= 1;
		}

		return statIncreasePerLevel;
	}
}
