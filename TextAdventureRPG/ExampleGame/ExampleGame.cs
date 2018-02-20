using System;
using System.Collections.Generic;

/**
 * EXAMPLE GAME
 * A small example of a game that can be made with this RPG framework. It contains all of
 * the functionality of the framework, but no real story to speak of.
 */
static class ExampleGame
{
	// The game will continue running until this bool is set to 'false'
	public static bool quitRequested = false;

	const int maxHeroCount = 4;
	const int costOfHeroByLevel = 100;

	// Define the three "tiers" of monsters that can be battled
	static RandomEncounterGenerator beginnerTier = new RandomEncounterGenerator(
		new Enemy[] {
			ExampleEnemies.rat,
			ExampleEnemies.spider,
			ExampleEnemies.slime },
		1, 2);
	static RandomEncounterGenerator intermediateTier = new RandomEncounterGenerator(
		new Enemy[] {
			ExampleEnemies.orc,
			ExampleEnemies.hellhound,
			ExampleEnemies.gorgon },
		1, 3);
	static RandomEncounterGenerator advancedTier = new RandomEncounterGenerator(
		new Enemy[] {
			ExampleEnemies.dragon,
			ExampleEnemies.harpy,
			ExampleEnemies.lich },
		2, 5);

	// AbilityStoreItem - specifies an ability that can be purchased and its price.
	struct AbilityStoreItem
	{
		public Ability ability;
		public int price;

		public AbilityStoreItem(Ability ability, int price)
		{
			this.ability = ability;
			this.price = price;
		}
	}

	// The list of ability store items that can be purchased
	static AbilityStoreItem[] abilityStore = new AbilityStoreItem[]
	{
		new AbilityStoreItem(ExampleAbilities.heavyAttack, 100),
		new AbilityStoreItem(ExampleAbilities.spinAttack, 300),
		new AbilityStoreItem(ExampleAbilities.supportAlly, 400),
		new AbilityStoreItem(ExampleAbilities.heal, 50),
		new AbilityStoreItem(ExampleAbilities.healAll, 300),
		new AbilityStoreItem(ExampleAbilities.focus, 50),
		new AbilityStoreItem(ExampleAbilities.energyShower, 300),
		new AbilityStoreItem(ExampleAbilities.magicBlast, 400)
	};

	// Call this function to play the Example Game!
	public static void Run()
	{
		// Introduce the plot of the game...
		Console.WriteLine("Legions of monsters have appeared across the land!");
		Console.WriteLine("A hero must rise to fight them back.");
		Program.PressEnterToContinue();

		// Create the main character and add them to the party...
		PlayerCharacter mainCharacter = CustomCharacterCreator.CreatePlayerCharacter();
		PlayerData.party.Add(mainCharacter);

		// Main loop of the game where the player decides what they'd like to do next...
		while (quitRequested == false)
		{
			Console.Clear();

			// Show the player their options of what they can do next
			Console.WriteLine("What would you like to do?");
			Console.WriteLine("");
			Console.WriteLine("[1] Battle monsters");
			Console.WriteLine("[2] View hero stats");
			Console.WriteLine("[3] Learn a new ability");
			Console.WriteLine("[4] Hire a new hero");
			Console.WriteLine("[Q] Quit game");

			// Read the player's input and call whichever function goes
			// with the selection that they made.
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

			// If the user chose to quit, verify that they meant to before shutting down.
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
	}

	// Trigger an enemy encounter - allow the player to select the difficulty of battle that
	// they wish to engage in, then create a new encounter using a random encounter generator.
	static void BattleMonsters()
	{
		Console.Clear();

		// Ask the player what difficult level they want to fight at
		Console.WriteLine("BATTLE!");
		Console.WriteLine("");
		Console.WriteLine("Which tier of monsters will you challenge?");
		Console.WriteLine("[1] Beginner Tier");
		Console.WriteLine("[2] Intermediate Tier");
		Console.WriteLine("[3] Advanced Tier");
		Console.WriteLine("[X] Cancel");

		string userInput = Console.ReadLine().ToLower();
		EnemyEncounter encounter = null;

		// Create an encounter based on the player's selection
		if (userInput == "1")
		{
			encounter = beginnerTier.CreateEncounter();
		}
		else if (userInput == "2")
		{
			encounter = intermediateTier.CreateEncounter();
		}
		else if (userInput == "3")
		{
			encounter = advancedTier.CreateEncounter();
		}
		else if (userInput != "x")
		{
			Console.WriteLine("Invalid selection.");
			Program.PressEnterToContinue();
		}

		// If an enemy encounter was created, start it now!
		if (encounter != null)
		{
			encounter.Start();
		}
	}

	// Display a hero's stats
	static void ViewHeroStats()
	{
		Console.Clear();
		Console.WriteLine("VIEW HERO STATS");
		Console.WriteLine("");
		Console.WriteLine("Whose stats would you like to view?");
		Console.WriteLine("");

		// List out all of the heroes in the player's party.
		for (int i = 0; i < PlayerData.party.Count; i++)
		{
			int heroNumber = i + 1;
			Console.WriteLine("[" + heroNumber + "] " + PlayerData.party[i].name);
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
		if (heroIndexValid && selectedHeroNumber > 0 && selectedHeroNumber <= PlayerData.party.Count)
		{
			PlayerCharacter selectedHero = PlayerData.party[selectedHeroNumber - 1];
			selectedHero.DisplayStats();
		}

		// If the player made an invalid selection, notify them.
		else
		{
			Console.WriteLine("Invalid selection.");
			Program.PressEnterToContinue();
		}
	}

	// Show the ability store - the player can purchase abilities here and teach them to their party members
	static void BuyAbility()
	{
		Console.Clear();
		Console.WriteLine("LEARN NEW ABILITIES");
		Console.WriteLine("");
		Console.WriteLine("Which ability are you interested in learning?");
		Console.WriteLine("(You have " + PlayerData.gold + " gold)");
		Console.WriteLine("");

		// Display all of the abilities in the store
		for (int i = 0; i < abilityStore.Length; i++)
		{
			Console.WriteLine("[" + (i + 1) + "] " + abilityStore[i].ability.name + " (" + abilityStore[i].price + " gold)");
		}
		Console.WriteLine("[X] Cancel");

		// Get the user's input to see which ability they want to purchase
		string userInput = Console.ReadLine().ToLower();
		int numericInput;
		bool validNumberEntered = int.TryParse(userInput, out numericInput);
		
		// If the selected ability is valid, continue...
		if (validNumberEntered && numericInput > 0 && numericInput <= abilityStore.Length)
		{
			AbilityStoreItem selectedItem = abilityStore[numericInput - 1];

			// If the player doesn't have enough gold to purchase the selected ability, leave the shop
			if (selectedItem.price > PlayerData.gold)
			{
				Console.WriteLine("You don't have enough gold!");
				Program.PressEnterToContinue();
				return;
			}

			// Get a list of the heroes who don't know this ability yet
			List<PlayerCharacter> possibleRecipients = new List<PlayerCharacter>();

			foreach (PlayerCharacter hero in PlayerData.party)
			{
				if (hero.abilities.Contains(selectedItem.ability) == false)
				{
					possibleRecipients.Add(hero);
				}
			}

			// If there are heroes who can learn this ability, show the ability's description
			if (possibleRecipients.Count > 0)
			{
				Console.Clear();
				Console.WriteLine(selectedItem.ability.name.ToUpper());
				Console.WriteLine("");
				Console.WriteLine(selectedItem.ability.menuDescription);
				Console.WriteLine(selectedItem.ability.GetCostDescription());
				Console.WriteLine("");
				Console.WriteLine("PRICE: " + selectedItem.price + " gold");
				Console.WriteLine("(You have " + PlayerData.gold + " gold)");
				Console.WriteLine("");
				Console.WriteLine("Who should learn this ability?");

				// List out all of the heroes in the player's party.
				for (int i = 0; i < possibleRecipients.Count; i++)
				{
					int heroNumber = i + 1;
					Console.WriteLine("[" + heroNumber + "] " + possibleRecipients[i].name);
				}
				Console.WriteLine("[X] Cancel");

				// Get the user's input to see who should learn the ability
				userInput = Console.ReadLine().ToLower();

				// Back out of this function now if the player canceled.
				if (userInput == "x")
				{
					return;
				}

				// Translate the user's input to a numerical value so we can use it to identify which character was selected
				int selectedHeroNumber;
				bool heroIndexValid = int.TryParse(userInput, out selectedHeroNumber);

				// If a valid hero was selected, add the ability to their moveset
				if (heroIndexValid && selectedHeroNumber > 0 && selectedHeroNumber <= possibleRecipients.Count)
				{
					PlayerCharacter selectedHero = possibleRecipients[selectedHeroNumber - 1];
					selectedHero.abilities.Add(selectedItem.ability);
					PlayerData.gold -= selectedItem.price;

					Console.Clear();
					Console.WriteLine(selectedHero.name + " learned " + selectedItem.ability.name + "!");
					Program.PressEnterToContinue();
				}

				// If the player made an invalid selection, notify them.
				else
				{
					Console.WriteLine("Invalid selection.");
					Program.PressEnterToContinue();
				}
			}
			else
			{
				Console.WriteLine("Everyone on your party already knows the '" + selectedItem.ability + "' ability!");
				Program.PressEnterToContinue();
			}
		}

		// Quit the shop if the player didn't make a valid ability selection
		else if (userInput != "x")
		{
			Console.WriteLine("Invalid input.");
			Program.PressEnterToContinue();
		}
	}

	// Allow the player to pay gold to create new characters for their party. The higher the character's
	// initial level, the more expensive they are!
	static void HireHero()
	{
		Console.Clear();
		Console.WriteLine("HIRE A HERO");
		Console.WriteLine("");

		// Don't allow the player to hire a new hero if the party size limit is hit
		if (PlayerData.party.Count >= maxHeroCount)
		{
			Console.WriteLine("You cannot hire more heroes; your party is full!");
		}

		// Find out what the highest level in the hero party currently is.
		// That will be the highest level that the player is allowed to hire.
		int highestLevel = 1;

		foreach (PlayerCharacter hero in PlayerData.party)
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
			Program.PressEnterToContinue();
			return;
		}

		// Tell the player how much the hero will cost to hire (and how much gold they currently have)
		int price = desiredLevel * costOfHeroByLevel;
		Console.WriteLine("A level " + desiredLevel + " hero will cost you " + price + " gold.");
		Console.WriteLine("(You currently have " + PlayerData.gold + " gold)");
		Console.WriteLine("");

		// If the player has enough gold, confirm whether they'd like to make the hire.
		if (PlayerData.gold >= price)
		{
			Console.WriteLine("Are you sure you want to make this purchase? (yes/no)");
			userInput = Console.ReadLine().ToLower();

			if (userInput == "yes" || userInput == "y")
			{
				PlayerCharacter newHero = CustomCharacterCreator.CreatePlayerCharacter();
				PlayerData.party.Add(newHero);

				Console.WriteLine("");
				Console.WriteLine(newHero.name + " has joined your party!");
				Program.PressEnterToContinue();
			}
		}

		// If the player can't afford the purchase, notify them.
		else
		{
			Console.WriteLine("Sorry, it looks like you can't afford this purchase yet. Come back later!");
			Program.PressEnterToContinue();
		}
	}
}
