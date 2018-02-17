using System;

class PlayerCharacter : Character
{
	private int level;
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

	public void SetLevel(int level)
	{
		this.level = level;

		int statMultiplier = level + (levelOneStatMultiplier - 1);

		baseStats = new Stats(
			statIncreasePerLevel.hp * statMultiplier,
			statIncreasePerLevel.sp * statMultiplier,
			statIncreasePerLevel.strength * statMultiplier,
			statIncreasePerLevel.magic * statMultiplier,
			statIncreasePerLevel.speed * statMultiplier);

		ResetStats();
	}

	public void LevelUp()
	{
		SetLevel(level + 1);

		Console.Clear();
		Console.WriteLine(name.ToUpper() + " HAS REACHED LEVEL " + level.ToString() + "!");
		Console.WriteLine("");
		Console.WriteLine("Max HP increased by " + statIncreasePerLevel.hp + "!");
		Console.WriteLine("Max SP increased by " + statIncreasePerLevel.sp + "!");
		Console.WriteLine("Strength increased by " + statIncreasePerLevel.strength + "!");
		Console.WriteLine("Magic increased by " + statIncreasePerLevel.magic + "!");
		Console.WriteLine("Speed increased by " + statIncreasePerLevel.speed + "!");
		Program.PressEnterToContinue();
	}
}
