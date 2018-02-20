using System;

/**
 * CUSTOM CHARACTER CREATOR
 * A static class used to allow the player to create their own characters.
 */
static class CustomCharacterCreator
{
	// Trigger the character creation process.
	public static PlayerCharacter CreatePlayerCharacter()
	{
		// Prompt the use to enter a name for the new character
		string name = PromptForName();

		// Prompt the user to specify the character's pronouns
		Pronouns pronouns = PromptForPronouns();

		// Prompt for the character's strongest and weakest traits, which will determine their stats
		Stats statIncreasePerLevel = PromptForStats();

		// Create and return the new character using the information specified by the user
		PlayerCharacter character = new PlayerCharacter(name, pronouns, statIncreasePerLevel);
		return character;
	}
	
	// Ask the player for the character's name
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

	// Ask the player for the character's gender pronouns
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

	// If the player indicated "other" for their gender, provide them a list of pre-existing non-binary pronouns
	// or allow them to specify their own.
	private static Pronouns PromptForNonbinaryPronouns()
	{
		// Write out all of the non-binary pronoun presets for the player to choose from
		Console.Clear();
		Console.WriteLine("What are your character's pronouns?");
		for (int i = 0; i < Pronouns.Presets.nonbinary.Length; i++)
		{
			Console.WriteLine("[" + (i + 1) + "] " + Pronouns.Presets.nonbinary[i].they + " / " + Pronouns.Presets.nonbinary[i].them);
		}
		Console.WriteLine("[C] Custom");
		Pronouns pronouns = null;

		// Get the player's selection
		while (pronouns == null)
		{
			string userInput = Console.ReadLine().ToUpper();

			int numericInput;
			bool isInputNumeric = int.TryParse(userInput, out numericInput);

			// Use a preset if the player entered a valid number
			if (isInputNumeric && numericInput >= 1 && numericInput <= Pronouns.Presets.nonbinary.Length)
			{
				pronouns = Pronouns.Presets.nonbinary[numericInput - 1];
			}

			// Allow the player to write a custom pronoun set
			else if (userInput == "C")
			{
				pronouns = Pronouns.CreateCustom();
			}

			// Inform the player if their input can't be used
			else
			{
				Console.WriteLine("Invalid input.");
			}
		}

		return pronouns;
	}

	// Ask the player what they'd like their strongest and weakest traits to be between strength,
	// magic and speed. The character's stats are generated based on the player's choices.
	private static Stats PromptForStats()
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
		Stats statIncreasePerLevel = new Stats(10, 10, 3, 3, 3);

		// Boost whichever traits were chosen to be the strongest...
		if (strongestTrait == "S")
		{
			// Boost physical traits...
			statIncreasePerLevel.Add(Stats.Type.HP, 1);
			statIncreasePerLevel.Add(Stats.Type.Strength, 1);
		}
		else if (strongestTrait == "M")
		{
			// Boost magical traits...
			statIncreasePerLevel.Add(Stats.Type.SP, 2);
			statIncreasePerLevel.Add(Stats.Type.Magic, 1);
		}
		else if (strongestTrait == "A")
		{
			// Boost speed trait...
			statIncreasePerLevel.Add(Stats.Type.Speed, 1);
		}

		// Reduce whichever traits were chosen to be the weakest...
		if (weakestTrait == "S")
		{
			// Reduce physical traits...
			statIncreasePerLevel.Subtract(Stats.Type.HP, 1);
			statIncreasePerLevel.Subtract(Stats.Type.Strength, 1);
		}
		else if (weakestTrait == "M")
		{
			// Reduce magical traits...
			statIncreasePerLevel.Subtract(Stats.Type.SP, 2);
			statIncreasePerLevel.Subtract(Stats.Type.Magic, 1);
		}
		else if (weakestTrait == "A")
		{
			// Reduce speed trait...
			statIncreasePerLevel.Subtract(Stats.Type.Speed, 1);
		}

		return statIncreasePerLevel;
	}
}
