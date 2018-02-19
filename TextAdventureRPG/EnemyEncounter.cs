using System;

class EnemyEncounter
{
	protected Enemy[] enemies;
	bool canRun;
	bool battleComplete = false;

	public EnemyEncounter(Enemy[] enemies, bool canRun)
	{
		this.enemies = enemies;
		this.canRun = canRun;
	}

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

		while (battleComplete == false)
		{
			// Find out which character should act next based on their speed values
			Character nextCharacter = GetCharacterForNextTurn();

			// Allow abilities with temporary effects to wear off over time
			foreach (Ability ability in nextCharacter.abilities)
			{
				if (ability is TemporaryAbility)
				{
					((TemporaryAbility)ability).OnUserTurnStart();
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
	}

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

	void PlayerTurn(PlayerCharacter hero)
	{
		Console.WriteLine(hero.name.ToUpper() + " IS READY!");
		Console.WriteLine("");
		Console.WriteLine("What will " + hero.pronouns.they + " do?");

		for (int i = 0; i < hero.abilities.Count; i++)
		{
			Console.WriteLine("[" + (i + 1) + "] " + hero.abilities[i].name);
		}
		Console.WriteLine("[R] Run");

		bool turnComplete = false;
		while (turnComplete == false)
		{
			string userInput = Console.ReadLine().ToUpper();

			int numericInput;
			bool isInputNumeric = int.TryParse(userInput, out numericInput);

			if (isInputNumeric && numericInput >= 1 && numericInput <= hero.abilities.Count)
			{
				Ability abilityToUse = hero.abilities[numericInput - 1];

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
			else
			{
				Console.WriteLine("Invalid input.");
			}
		}
		Program.PressEnterToContinue();
	}

	void EnemyTurn(Enemy enemy)
	{
		Ability abilityToUse = enemy.abilities[Program.random.Next(enemy.abilities.Count)];

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
		Program.PressEnterToContinue();
	}

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

	void OnAllEnemiesDefeated()
	{
		battleComplete = true;

		Console.Clear();
		Console.WriteLine(PlayerData.party[0].name + "'s party is victorious!");
		Program.PressEnterToContinue();
	}
}