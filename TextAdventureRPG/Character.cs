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

	public class Stats
	{
		public Stats(int hp, int sp, int strength, int magic, int speed)
		{
			this.hp = hp;
			this.sp = sp;
			this.strength = strength;
			this.magic = magic;
			this.speed = speed;
		}

		public int hp;
		public int sp;

		public int strength;
		public int magic;
		public int speed;
	}

	public Character(string name, Pronouns pronouns)
	{
		this.name = name;
		this.pronouns = pronouns;
	}

	public void ResetStats()
	{
		currentStats = new Stats(baseStats.hp, baseStats.sp, baseStats.strength, baseStats.magic, baseStats.speed);
		turnPriority = 0;
	}

	public void DisplayStats()
	{
		Console.Clear();
		Console.WriteLine(name.ToUpper() + "'S STATS");
		Console.WriteLine("");
		Console.WriteLine("HP: " + currentStats.hp + " / " + baseStats.hp);
		Console.WriteLine("SP: " + currentStats.sp + " / " + baseStats.sp);
		Console.WriteLine("Strength: " + currentStats.strength);
		Console.WriteLine("Magic: " + currentStats.magic);
		Console.WriteLine("Speed: " + currentStats.speed);
		Program.PressEnterToContinue();
	}

	public int GetPhysicalDefense()
	{
		return currentStats.strength / 2;
	}

	public int GetMagicalDefense()
	{
		return currentStats.magic / 2;
	}
}