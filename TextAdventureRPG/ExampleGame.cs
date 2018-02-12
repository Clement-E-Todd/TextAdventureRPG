using System;
using System.Collections.Generic;

static class ExampleGame
{
	public static List<Character> heroes = new List<Character>();
	public static int gold = 100;
	public static bool quitRequested = false;

	const int maxHeroCount = 4;
	const int costOfHeroByLevel = 250;

	public static void Run()
	{
		// Introduce the plot of the game...
		Console.WriteLine("The Dark Lord has risen, spreading evil across the land!");
		Console.WriteLine("A hero must rise to defeat his legions of monsters.");
		Console.WriteLine("");
		Console.WriteLine("[Press ENTER to continue]");
		Console.ReadLine();

		// Create the main character and add them to the party...
		Character mainCharacter = Character.CreatePlayerCharacter();
		heroes.Add(mainCharacter);

		// Main loop of the game where the player decides what they'd like to do next...
		while (quitRequested == false)
		{
			Console.Clear();

			Console.WriteLine("What would you like to do?");
			Console.WriteLine("");
			Console.WriteLine("[1] Battle monsters");
			Console.WriteLine("[2] View hero stats");
			Console.WriteLine("[3] Buy a new ability");
			Console.WriteLine("[4] Hire a new hero");
			Console.WriteLine("[Q] Quit game");

			string userInput = Console.ReadLine().ToLower();

			if (userInput == "1")
			{
				BattleMonsters();
			}
			else if (userInput == "2")
			{
				ViewHeroStats();
			}
			else if (userInput == "3")
			{
				BuyAbility();
			}
			else if (userInput == "4")
			{
				HireHero();
			}
			else if (userInput == "q")
			{
				Console.WriteLine("Are you sure you want to quit? (yes/no)");
				userInput = Console.ReadLine().ToLower();

				if (userInput == "yes" || userInput == "y")
				{
					quitRequested = true;
				}
			}
		}

		// Pause before ending the game...
		Console.WriteLine("[Press ENTER to end the game]");
		Console.ReadLine();
	}

	static void BattleMonsters()
	{
	}

	static void ViewHeroStats()
	{
		Console.Clear();
		Console.WriteLine("VIEW HERO STATS");
		Console.WriteLine("");
		Console.WriteLine("Whose stats would you like to view?");
		Console.WriteLine("");

		// List out all of the heroes in the player's party.
		for (int i = 0; i < heroes.Count; i++)
		{
			int heroNumber = i + 1;
			Console.WriteLine("[" + heroNumber + "] " + heroes[i].name);
		}
		Console.WriteLine("[X] Cancel");

		// Get the user's input
		string userInput = Console.ReadLine().ToLower();

		// Back out of this function now if the player canceled.
		if (userInput == "x")
		{
			return;
		}

		// Translate the user's input to a numerical value so we can use it to identify which character was selected
		int selectedHeroNumber;
		bool heroIndexValid = int.TryParse(userInput, out selectedHeroNumber);

		// If a valid hero was selected, show their stats
		if (heroIndexValid && selectedHeroNumber > 0 && selectedHeroNumber <= heroes.Count)
		{
			Character selectedHero = heroes[selectedHeroNumber - 1];
			selectedHero.DisplayStats();
		}

		// If the player made an invalid selection, notify them.
		else
		{
			Console.WriteLine("Invalid selection.");
			Console.WriteLine("");
			Console.WriteLine("[Press ENTER to continue]");
			Console.ReadLine();
		}
	}

	static void HireHero()
	{
		Console.Clear();
		Console.WriteLine("HIRE A HERO");
		Console.WriteLine("");

		// Don't allow the player to hire a new hero if the party size limit is hit
		if (heroes.Count >= maxHeroCount)
		{
			Console.WriteLine("You cannot hire more heroes; your party is full!");
		}

		// Find out what the highest level in the hero party currently is.
		// That will be the highest level that the player is allowed to hire.
		int highestLevel = 1;

		foreach (Character hero in heroes)
		{
			if (hero.GetLevel() > highestLevel)
			{
				highestLevel = hero.GetLevel();
			}
		}
		
		// Ask the player what level of hero they'd like to hire...
		Console.WriteLine("What level of hero would you like to hire?");
		Console.WriteLine("Remember, you can only have up to four heroes in your party!");
		Console.WriteLine("(Maximum hero level: " + highestLevel + ")");

		string userInput = Console.ReadLine();
		int desiredLevel;
		bool validNumberEntered = int.TryParse(userInput, out desiredLevel);

		// If the user's input is not valid, boot them out of the hiring process...
		if (validNumberEntered == false || desiredLevel < 1 || desiredLevel > highestLevel)
		{
			Console.WriteLine("You can't hire a hero at that level!");
			Console.WriteLine("");
			Console.WriteLine("[Press ENTER to continue]");
			Console.ReadLine();
			return;
		}

		// Tell the player how much the hero will cost to hire (and how much gold they currently have)
		int price = desiredLevel * costOfHeroByLevel;
		Console.WriteLine("A level " + desiredLevel + " hero will cost you " + price + " gold.");
		Console.WriteLine("(You currently have " + gold + " gold)");
		Console.WriteLine("");

		// If the player has enough gold, confirm whether they'd like to make the hire.
		if (gold >= price)
		{
			Console.WriteLine("Are you sure you want to make this purchase? (yes/no)");
			userInput = Console.ReadLine().ToLower();

			if (userInput == "yes" || userInput == "y")
			{
				Character newHero = Character.CreatePlayerCharacter();
				heroes.Add(newHero);

				Console.WriteLine("");
				Console.WriteLine(newHero.name + " has joined your party!");
				Console.WriteLine("");
				Console.WriteLine("[Press ENTER to continue]");
				Console.ReadLine();
			}
		}

		// If the player can't afford the purchase, notify them.
		else
		{
			Console.WriteLine("Sorry, it looks like you can't afford this purchase yet. Come back later!");
			Console.WriteLine("");
			Console.WriteLine("[Press ENTER to continue]");
			Console.ReadLine();
		}
	}

	static void BuyAbility()
	{
	}
}
