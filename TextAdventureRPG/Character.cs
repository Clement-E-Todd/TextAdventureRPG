using System;
using System.Collections.Generic;

abstract class Character
{
	public string name;
	public Pronouns pronouns;

	public int turnPriority = 0;

	public Stats baseStats;
	public Stats currentStats;

	public List<Ability> abilities = new List<Ability>();

	public Character(string name, Pronouns pronouns)
	{
		this.name = name;
		this.pronouns = pronouns;
	}

	public void ResetStats()
	{
		currentStats = baseStats.CreateCopy();
		turnPriority = 0;
	}

	public void DisplayStats()
	{
		Console.Clear();
		Console.WriteLine(name.ToUpper() + "'S STATS");
		Console.WriteLine("");
		Console.WriteLine("HP: " + currentStats.Get(Stats.Type.HP) + " / " + baseStats.Get(Stats.Type.HP));
		Console.WriteLine("SP: " + currentStats.Get(Stats.Type.SP) + " / " + baseStats.Get(Stats.Type.SP));
		Console.WriteLine("Strength: " + currentStats.Get(Stats.Type.Strength));
		Console.WriteLine("Magic: " + currentStats.Get(Stats.Type.Magic));
		Console.WriteLine("Speed: " + currentStats.Get(Stats.Type.Speed));
		Program.PressEnterToContinue();
	}

	public int GetPhysicalDefense()
	{
		return currentStats.Get(Stats.Type.Strength) / 2;
	}

	public int GetMagicalDefense()
	{
		return currentStats.Get(Stats.Type.Magic) / 2;
	}
}