using System;

/**
 * PLAYER CHARACTER
 * A character that can be added to the player's party. This class extends the Character class
 * by adding the capability to gain experience points and level up after a battle.
 */
class PlayerCharacter : Character
{
	private int level;
	public int exp = 0;
	const int levelOneStatMultiplier = 5;

	public Stats statIncreasePerLevel;

	// PlayerCharacter constructor - specifies the amount by which the character's stats should increase each time
	// they level up and also what level the character should start at.
	public PlayerCharacter(string name, Pronouns pronouns, Stats statIncreasePerLevel, int level = 1) : base(name, pronouns)
	{
		this.name = name;
		this.pronouns = pronouns;
		this.statIncreasePerLevel = statIncreasePerLevel;

		abilities.Add(StandardAbilities.attack);
		abilities.Add(StandardAbilities.defend);

		SetLevel(level);
	}

	// Get the player's current level. We could arguably just make the 'level' variable
	// public, but we don't want to allow the level to be set directly from outside.
	// See the "SetLevel" function for more details.
	public int GetLevel()
	{
		return level;
	}

	// Calculate how much total experience is needed to reach the next level. The formula
	// for this calculation is explained below.
	public int GetExpForNextLevel()
	{
		// The experience needed to level up is equal to the exp needed for the previous
		// level plus ten times the current level squared. Examples:
		// At level 1, you need 10 exp to reach level 2. (0 + (10 * 1 * 1))
		// At level 2, you need 50 exp to reach level 3. (10 + (10 * 2 * 2))
		// At level 3, you need 140 exp to reach level 4. (50 + (10 * 3 * 3))
		// At level 4, you need 300 exp to reach level 5. (140 + (10 * 4 * 4))
		// At level 5, you need 550 exp to reach level 6. (300 + (10 * 5 * 5))
		// etc...
		int totalExpNeeded = 0;

		for (int i = 0; i <= level; i++)
		{
			totalExpNeeded += i * i * 10;
		}

		return totalExpNeeded;
	}

	// Set the character's current level. We don't allow the level variable to be set
	// directly from outside of this class because we need to also update the character's
	// sttats any time their level changes.
	public void SetLevel(int level)
	{
		this.level = level;

		int statMultiplier = level + (levelOneStatMultiplier - 1);

		baseStats = statIncreasePerLevel.CreateCopy(statMultiplier);

		ResetStats();
	}

	// Display a write-up of all of the character's stats.
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

	// Increase the character's level by one and display the stat increase gained to the player.
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
