using System;

public class Stats
{
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

	public Stats()
	{
		values = new int[(int)Type.TOTAL_TYPES];
	}

	public Stats(int hp, int sp, int strength, int magic, int speed)
	{
		values = new int[] { hp, sp, strength, magic, speed };
	}

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

	public int Get(Type stat)
	{
		return values[(int)stat];
	}

	public void Set(Type stat, int value)
	{
		values[(int)stat] = Math.Max(value, 0);
	}

	public void Add(Type stat, int amount)
	{
		Set(stat, values[(int)stat] + amount);
	}

	public void Subtract(Type stat, int amount)
	{
		Set(stat, values[(int)stat] - amount);
	}
}