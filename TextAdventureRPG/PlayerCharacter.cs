using System;

class PlayerCharacter : Character
{
	private int level;
	public int exp = 0;
	const int levelOneStatMultiplier = 5;

	public Stats statIncreasePerLevel;

	public PlayerCharacter(string name, Pronouns pronouns, Stats statIncreasePerLevel, int level = 1) : base(name, pronouns)
	{
		this.name = name;
		this.pronouns = pronouns;
		this.statIncreasePerLevel = statIncreasePerLevel;

		abilities.Add(AbilityDatabase.attack);
		abilities.Add(AbilityDatabase.defend);

		SetLevel(level);
	}

	public int GetLevel()
	{
		return level;
	}

	public int GetExpForNextLevel()
	{
		// The experience needed to level up is equal to ten times the current level squared. Examples:
		// At level 1, you need 10 exp to reach level 2.
		// At level 2, you need 40 exp to reach level 3.
		// At level 3, you need 90 exp to reach level 4.
		// etc...
		return level * level * 10;
	}

	public void SetLevel(int level)
	{
		this.level = level;

		int statMultiplier = level + (levelOneStatMultiplier - 1);

		baseStats = statIncreasePerLevel.CreateCopy(statMultiplier);

		ResetStats();
	}

	public void DisplayStats()
	{
		Console.Clear();
		Console.WriteLine(name.ToUpper() + "'S STATS");
		Console.WriteLine("");
		Console.WriteLine("Level: " + level);
		Console.WriteLine("EXP: " + exp);
		Console.WriteLine("EXP needed for next level: " + (GetExpForNextLevel() - exp));
		Console.WriteLine("");
		Console.WriteLine("HP: " + currentStats.Get(Stats.Type.HP) + " / " + baseStats.Get(Stats.Type.HP));
		Console.WriteLine("SP: " + currentStats.Get(Stats.Type.SP) + " / " + baseStats.Get(Stats.Type.SP));
		Console.WriteLine("Strength: " + currentStats.Get(Stats.Type.Strength));
		Console.WriteLine("Magic: " + currentStats.Get(Stats.Type.Magic));
		Console.WriteLine("Speed: " + currentStats.Get(Stats.Type.Speed));
		Program.PressEnterToContinue();
	}

	public void LevelUp()
	{
		SetLevel(level + 1);

		Console.Clear();
		Console.WriteLine(name.ToUpper() + " HAS REACHED LEVEL " + level.ToString() + "!");
		Console.WriteLine("");
		Console.WriteLine("Max HP increased by " + statIncreasePerLevel.Get(Stats.Type.HP) + "!");
		Console.WriteLine("Max SP increased by " + statIncreasePerLevel.Get(Stats.Type.SP) + "!");
		Console.WriteLine("Strength increased by " + statIncreasePerLevel.Get(Stats.Type.Strength) + "!");
		Console.WriteLine("Magic increased by " + statIncreasePerLevel.Get(Stats.Type.Magic) + "!");
		Console.WriteLine("Speed increased by " + statIncreasePerLevel.Get(Stats.Type.Speed) + "!");
		Program.PressEnterToContinue();
	}
}
