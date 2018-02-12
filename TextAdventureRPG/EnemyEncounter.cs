using System;

class EnemyEncounter
{
	protected Character[] enemies;

	public EnemyEncounter(Character[] enemies)
	{
		this.enemies = enemies;
	}

	public bool Start()
	{
		Console.Clear();
		Console.WriteLine("ENEMY ENCOUNTER!");
		Console.WriteLine("");

		foreach (Character enemy in enemies)
		{
			Console.WriteLine("VS Level " + enemy.GetLevel() + " " + enemy.name + "!");
		}

		Console.WriteLine("");
		Console.WriteLine("(Battle system not yet implemented)");
		Console.WriteLine("[Press ENTER to continue]");

		Console.ReadLine();

		return true;
	}
}