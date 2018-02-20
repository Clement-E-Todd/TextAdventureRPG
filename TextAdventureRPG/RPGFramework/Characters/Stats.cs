using System;

/**
 * STATS
 * Represents the statistics used by a character in battle
 */
public class Stats
{
	// All of the stats are contained in a single array. The Type enum
	// is used to indicate which index each stat should be stored in.
	private int[] values;
	public enum Type
	{
		HP,
		SP,
		Strength,
		Magic,
		Speed,
		TOTAL_TYPES
	}

	// Stats constructor with no parameters - creates a new set of stats with all
	// of the values defaulted to 0
	public Stats()
	{
		values = new int[(int)Type.TOTAL_TYPES];
	}

	// Stats constructor with parameters - specifies all of the values that the stats
	// set should start with.
	public Stats(int hp, int sp, int strength, int magic, int speed)
	{
		values = new int[] { hp, sp, strength, magic, speed };
	}

	// Create a copy of the stats set. An example of how this can be used to to quickly
	// set a character's current stats to match their base stats.
	public Stats CreateCopy(float multiplier = 1.0f)
	{
		return new Stats(
			(int)(values[(int)Type.HP] * multiplier),
			(int)(values[(int)Type.SP] * multiplier),
			(int)(values[(int)Type.Strength] * multiplier),
			(int)(values[(int)Type.Magic] * multiplier),
			(int)(values[(int)Type.Speed] * multiplier)
		);
	}

	// Get a specific value from this stats set
	public int Get(Type stat)
	{
		return values[(int)stat];
	}

	// Set a specific value in this stats set
	public void Set(Type stat, int value)
	{
		values[(int)stat] = Math.Max(value, 0);
	}

	// Add a specified amount to a value in this stats set
	public void Add(Type stat, int amount)
	{
		Set(stat, values[(int)stat] + amount);
	}

	// Subtract a specified amount from a value in this stats set
	public void Subtract(Type stat, int amount)
	{
		Set(stat, values[(int)stat] - amount);
	}
}