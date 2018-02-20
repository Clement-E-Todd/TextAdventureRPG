using System;
using System.Collections.Generic;

/**
 * ENEMY ENCOUNTER
 * Represents a battle. This class can be insantiated directly for scripted battles or generated
 * by the static RandomEncounterGenerator class for randomized battles.
 */
class EnemyEncounter
{
	protected Enemy[] enemies;
	bool canRun;
	bool battleComplete = false;

	// EnemyEncounter constructor - Simply specify which enemies should appear and whther or not
	// the heroes are able to escape from the battle!
	public EnemyEncounter(Enemy[] enemies, bool canRun)
	{
		this.enemies = enemies;
		this.canRun = canRun;
	}

	// Run an enemy encounter from start to finish
	public void Start()
	{
		Console.Clear();
		Console.WriteLine("ENEMY ENCOUNTER!");
		Console.WriteLine("");

		foreach (Enemy enemy in enemies)
		{
			Console.WriteLine("VS " + enemy.name + "!");
		}

		Program.PressEnterToContinue();

		// This loop represents characters taking their turn to act - each iteration through
		// the loop is one character's turn.
		while (battleComplete == false)
		{
			// Find out which character should act next based on their speed values
			Character nextCharacter = GetCharacterForNextTurn();

			// Allow abilities with temporary effects to wear off over time
			foreach (Ability ability in nextCharacter.abilities)
			{
				if (ability is TemporaryAbility)
				{
					((TemporaryAbility)ability).OnCharacterTurnStart(nextCharacter);
				}
			}

			// Write out the stats of the characters in the battle
			Console.Clear();
			WriteCurrentStats();

			// Perform the character's turn
			if (nextCharacter is PlayerCharacter)
			{
				PlayerTurn((PlayerCharacter)nextCharacter);
			}
			else
			{
				EnemyTurn((Enemy)nextCharacter);
			}

			// Check whether wither team has been defeated
			CheckForBattleCompletion();
		}

		// After the battle is complete, reset all of the heroes' stats
		foreach (PlayerCharacter hero in PlayerData.party)
		{
			hero.ResetStats();
		}
	}

	// Display the stats of all characters on both teams.
	private void WriteCurrentStats()
	{
		// Write the hero party's stats...
		Console.WriteLine(PlayerData.party[0].name.ToUpper() + "'s PARTY");
		foreach (PlayerCharacter hero in PlayerData.party)
		{
			// Darken the hero's stats text if they are unconscious
			if (hero.currentStats.Get(Stats.Type.HP) == 0)
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Gray;
			}

			// Write the hero's stats...
			Console.WriteLine(
				hero.name +
				"   HP: " + hero.currentStats.Get(Stats.Type.HP) + " / " + hero.baseStats.Get(Stats.Type.HP) +
				"   SP: " + hero.currentStats.Get(Stats.Type.SP) + " / " + hero.baseStats.Get(Stats.Type.SP) +
				"   Strength: " + hero.currentStats.Get(Stats.Type.Strength) +
				"   Magic: " + hero.currentStats.Get(Stats.Type.Magic) +
				"   Speed: " + hero.currentStats.Get(Stats.Type.Speed));
		}
		Console.WriteLine("");

		// Write the enemy party's stats...
		Console.WriteLine("ENEMIES");
		foreach (Enemy enemy in enemies)
		{
			// Darken the text color if this enemy was defeated
			if (enemy.currentStats.Get(Stats.Type.HP) == 0)
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Gray;
			}

			// Write the enemy's stats...
			Console.WriteLine(
				enemy.name +
				"   HP: " + enemy.currentStats.Get(Stats.Type.HP) + " / " + enemy.baseStats.Get(Stats.Type.HP) +
				"   SP: " + enemy.currentStats.Get(Stats.Type.SP) + " / " + enemy.baseStats.Get(Stats.Type.SP));
		}
		Console.WriteLine("");

		// Reset the text color
		Console.ForegroundColor = ConsoleColor.Gray;
	}

	// Find out which character should act next. The higher a character's speed stat, the more frequently they will act.
	Character GetCharacterForNextTurn()
	{
		Character nextCharacter = null;
		int highestPriority = 0;

		foreach (Character hero in PlayerData.party)
		{
			if (hero.currentStats.Get(Stats.Type.HP) > 0)
			{
				hero.turnPriority += hero.currentStats.Get(Stats.Type.Speed);
				if (hero.turnPriority > highestPriority)
				{
					highestPriority = hero.turnPriority;
					nextCharacter = hero;
				}
			}
		}

		foreach (Character enemy in enemies)
		{
			if (enemy.currentStats.Get(Stats.Type.HP) > 0)
			{
				enemy.turnPriority += enemy.currentStats.Get(Stats.Type.Speed);
				if (enemy.turnPriority > highestPriority)
				{
					highestPriority = enemy.turnPriority;
					nextCharacter = enemy;
				}
			}
		}

		nextCharacter.turnPriority = 0;
		return nextCharacter;
	}

	// Allow a player character to act by presenting the player with a choice of which ability to use.
	void PlayerTurn(PlayerCharacter hero)
	{
		Console.WriteLine(hero.name.ToUpper() + " IS READY!");
		Console.WriteLine("");
		Console.WriteLine("What will " + hero.pronouns.they + " do?");

		// Display all of the current hero's abilities to the user for them to choose from.
		for (int i = 0; i < hero.abilities.Count; i++)
		{
			string abilityMessage = "[" + (i + 1) + "] " + hero.abilities[i].name;
			string costMessage = hero.abilities[i].GetCostDescription();

			if (costMessage != null)
			{
				abilityMessage += " " + costMessage;
			}

			if (hero.abilities[i].CanCharacterAffordCosts(hero) == false)
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
			}

			Console.WriteLine(abilityMessage);
			Console.ForegroundColor = ConsoleColor.Gray;
		}
		Console.WriteLine("[R] Run");

		// Read the player's input and attempt to perform the selected ability. If the
		// player preovides iunvalid input, we will query again until they provide valid input.
		bool turnComplete = false;
		while (turnComplete == false)
		{
			string userInput = Console.ReadLine().ToUpper();

			int numericInput;
			bool isInputNumeric = int.TryParse(userInput, out numericInput);

			// If the user typed a number and the number is in range, attempt to perform the selected ability
			if (isInputNumeric && numericInput >= 1 && numericInput <= hero.abilities.Count)
			{
				Ability abilityToUse = hero.abilities[numericInput - 1];

				// If the hero can afford the SP and HP costs of the ability, perform the ability now.
				if (abilityToUse.CanCharacterAffordCosts(hero))
				{
					Character[] targetParty = null;
					if (abilityToUse.targetType == Ability.TargetType.SingleAlly || abilityToUse.targetType == Ability.TargetType.AllAllies)
					{
						targetParty = PlayerData.party.ToArray();
					}
					else if (abilityToUse.targetType == Ability.TargetType.SingleOpponent || abilityToUse.targetType == Ability.TargetType.AllOpponents)
					{
						targetParty = enemies;
					}

					abilityToUse.Perform(hero, targetParty);

					turnComplete = true;
				}

				// If the hero can't afford the SP or HP cost of the ability, notify the polayer.
				else
				{
					Console.WriteLine(hero.name + " can't afford that ability right now.");
				}
			}

			// Attempt to run if the player typed "R". Running is always successful unless this encounter
			// disallows running altogether.
			else if (userInput == "R")
			{
				if (canRun)
				{
					Console.WriteLine(PlayerData.party[0].name + "'s party retreats!");
					battleComplete = true;
				}
				else
				{
					Console.WriteLine(PlayerData.party[0].name + "'s party is trapped!");
				}
				turnComplete = true;
			}

			// Notify the player if their input can't be used.
			else
			{
				Console.WriteLine("Invalid input.");
			}
		}

		// Pause at the end of the turn before continuing.
		Program.PressEnterToContinue();
	}

	// Allow an enemy character to act by rendomly selecting an ability and a target
	void EnemyTurn(Enemy enemy)
	{
		// Get a list of possible abilities for the enemy to perform - they cannot perform
		// abilities that they cannot afford the SP or HP cost for.
		List<Ability> possibleAbilities = new List<Ability>();
		foreach (Ability ability in enemy.abilities)
		{
			if (ability.CanCharacterAffordCosts(enemy))
			{
				possibleAbilities.Add(ability);
			}
		}

		// If there are any abilities that the enemy can perform currently, do so now by selecting one randomly.
		if (possibleAbilities.Count > 0)
		{
			Ability abilityToUse = possibleAbilities[Program.random.Next(possibleAbilities.Count)];

			Character[] targetParty = null;
			if (abilityToUse.targetType == Ability.TargetType.SingleAlly || abilityToUse.targetType == Ability.TargetType.AllAllies)
			{
				targetParty = enemies;
			}
			else if (abilityToUse.targetType == Ability.TargetType.SingleOpponent || abilityToUse.targetType == Ability.TargetType.AllOpponents)
			{
				targetParty = PlayerData.party.ToArray();
			}

			abilityToUse.Perform(enemy, targetParty);
		}

		// Notify the user if the enemy is unable to act.
		else
		{
			Console.WriteLine(enemy.name + " finds " + enemy.pronouns.themself + " unable to act.");
		}

		Program.PressEnterToContinue();
	}

	// Check whether either team has been defeated and, if so, end the battle accordingly.
	void CheckForBattleCompletion()
	{
		// Check the enemy party for surviving monsters. If none are found, the battle is won!
		bool undefeatedEnemyFound = false;
		foreach (Enemy enemy in enemies)
		{
			if (enemy.currentStats.Get(Stats.Type.HP) > 0)
			{
				undefeatedEnemyFound = true;
				break;
			}
		}

		if (undefeatedEnemyFound == false)
		{
			OnAllEnemiesDefeated();
			return;
		}

		// Check the hero party. If all are defeated, the battle is lost...
		bool undefeatedHeroFound = false;
		foreach (PlayerCharacter hero in PlayerData.party)
		{
			if (hero.currentStats.Get(Stats.Type.HP) > 0)
			{
				undefeatedHeroFound = true;
				break;
			}
		}

		if (undefeatedHeroFound == false)
		{
			battleComplete = true;

			Console.Clear();
			Console.WriteLine(PlayerData.party[0].name + "'s party was defeated...");
			Program.PressEnterToContinue();
		}
	}

	// Handle the victory state by giving rewards to the player.
	void OnAllEnemiesDefeated()
	{
		battleComplete = true;

		Console.Clear();
		Console.WriteLine(PlayerData.party[0].name + "'s party is victorious!");
		Console.WriteLine("");

		// Award gold to the player
		int totalGoldWon = 0;
		foreach (Enemy enemy in enemies)
		{
			totalGoldWon += enemy.goldReward;
		}
		PlayerData.gold += totalGoldWon;
		Console.WriteLine("Got " + totalGoldWon + " gold!");
		Console.WriteLine("");

		// Award exp to surviving heroes
		int totalExpGained = 0;
		foreach (Enemy enemy in enemies)
		{
			totalExpGained += enemy.expReward;
		}
		foreach (PlayerCharacter hero in PlayerData.party)
		{
			hero.exp += totalExpGained;
			Console.WriteLine(hero.name + " gained " + totalExpGained + " EXP!");
		}

		Program.PressEnterToContinue();

		// Level up heroes who have gained enough experience
		foreach (PlayerCharacter hero in PlayerData.party)
		{
			// Since it's possible to level up more than once in a single battle,
			// the experience check is done as a loop.
			while (hero.exp >= hero.GetExpForNextLevel())
			{
				hero.LevelUp();
			}
		}
	}
}