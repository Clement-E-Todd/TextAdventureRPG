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
			Character nextCharacter = GetCharacterForNextTurn();

			if (nextCharacter is PlayerCharacter)
			{
				PlayerTurn((PlayerCharacter)nextCharacter);
			}
			else
			{
				EnemyTurn((Enemy)nextCharacter);
			}
		}
	}

	Character GetCharacterForNextTurn()
	{
		Character nextCharacter = null;
		int highestPriority = 0;

		foreach (Character hero in PlayerData.party)
		{
			hero.turnPriority += hero.currentStats.speed;
			if (hero.turnPriority > highestPriority)
			{
				highestPriority = hero.turnPriority;
				nextCharacter = hero;
			}
		}

		foreach (Character enemy in enemies)
		{
			enemy.turnPriority += enemy.currentStats.speed;
			if (enemy.turnPriority > highestPriority)
			{
				highestPriority = enemy.turnPriority;
				nextCharacter = enemy;
			}
		}

		nextCharacter.turnPriority = 0;
		return nextCharacter;
	}

	void PlayerTurn(PlayerCharacter hero)
	{
		Console.Clear();
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
		Console.Clear();
		Console.WriteLine(enemy.name + " takes " + enemy.pronouns.their + " turn.");
		Program.PressEnterToContinue();
	}
}