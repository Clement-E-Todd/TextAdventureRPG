using System;

class Character
{
	public string name;
	public Pronouns gender;
	private int level;

	public class Stats
	{
		public int hp;
		public int sp;

		public int strength;
		public int magic;
		public int speed;
	}

	public Stats baseStats;
	public Stats currentStats;

	public Stats statIncreasePerLevel;

	const int levelOneStatMultiplier = 5;


	public Character(string name, Pronouns gender, Stats statIncreasePerLevel, int level = 1)
	{
		this.name = name;
		this.gender = gender;
		this.statIncreasePerLevel = statIncreasePerLevel;

		baseStats = new Stats();
		currentStats = new Stats();

		SetLevel(level);
	}

	public static Character CreatePlayerCharacter()
	{
		// Prompt the use to enter a name for the new character
		Console.Clear();
		Console.WriteLine("What is your character's name?");
		string name = "";

		while (name == "")
		{
			name = Console.ReadLine();
		}

		// Prompt the user to specify the character's gender
		Console.Clear();
		Console.WriteLine("What is your character's gender?");
		Console.WriteLine("[F] Female");
		Console.WriteLine("[M] Male");
		Console.WriteLine("[C] Customize");
		Pronouns gender = null;

		while (gender == null)
		{
			string userInput = Console.ReadLine().ToUpper();

			if (userInput == "F")
			{
				gender = Pronouns.Presets.feminine;
			}
			else if (userInput == "M")
			{
				gender = Pronouns.Presets.masculine;
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
		Stats statIncreasePerLevel = new Stats();

		statIncreasePerLevel.hp = 10;
		statIncreasePerLevel.sp = 10;

		statIncreasePerLevel.strength = 3;
		statIncreasePerLevel.magic = 3;
		statIncreasePerLevel.speed = 3;

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

		// Create and return the new character using the information specified by the user
		Character character = new Character(name, gender, statIncreasePerLevel);
		return character;
	}

	public Character CreateCopy()
	{
		Character characterCopy = new Character(name, gender, statIncreasePerLevel, level);

		return characterCopy;
	}

	public int GetLevel()
	{
		return level;
	}

	public void SetLevel(int level)
	{
		this.level = level;

		int statMultiplier = level + (levelOneStatMultiplier - 1);

		baseStats.hp = statIncreasePerLevel.hp * statMultiplier;
		baseStats.sp = statIncreasePerLevel.sp * statMultiplier;
		baseStats.strength = statIncreasePerLevel.strength * statMultiplier;
		baseStats.magic = statIncreasePerLevel.magic * statMultiplier;
		baseStats.speed = statIncreasePerLevel.speed * statMultiplier;

		ResetStats();
	}

	public void LevelUp()
	{
		SetLevel(level + 1);

		Console.Clear();
		Console.WriteLine(name.ToUpper() + " HAS REACHED LEVEL "+ level.ToString() + "!");
		Console.WriteLine("");
		Console.WriteLine("Max HP increased by " + statIncreasePerLevel.hp + "!");
		Console.WriteLine("Max SP increased by " + statIncreasePerLevel.sp + "!");
		Console.WriteLine("Strength increased by " + statIncreasePerLevel.strength + "!");
		Console.WriteLine("Magic increased by " + statIncreasePerLevel.magic + "!");
		Console.WriteLine("Speed increased by " + statIncreasePerLevel.speed + "!");
		Console.WriteLine("");
		Console.WriteLine("[Press ENTER to continue]");
		Console.ReadLine();
	}

	public void ResetStats()
	{
		currentStats.hp = baseStats.hp;
		currentStats.sp = baseStats.sp;
		currentStats.strength = baseStats.strength;
		currentStats.magic = baseStats.magic;
		currentStats.speed = baseStats.speed;
	}

	public void DisplayStats()
	{
		Console.Clear();
		Console.WriteLine(name.ToUpper() + "'S STATS");
		Console.WriteLine("");
		Console.WriteLine("HP: " + currentStats.hp + " / " + baseStats.hp);
		Console.WriteLine("SP: " + currentStats.sp + " / " + baseStats.sp);
		Console.WriteLine("Strength: " + currentStats.strength);
		Console.WriteLine("Magic: " + currentStats.magic);
		Console.WriteLine("Speed: " + currentStats.speed);
		Console.WriteLine("");
		Console.WriteLine("[Press ENTER to continue]");
		Console.ReadLine();
	}
}