using System;

class RandomEncounterGenerator
{
	Enemy[] possibleEnemies;

	int minimumEnemyCount;
	int maximumEnemyCount;

	public RandomEncounterGenerator(Enemy[] possibleEnemies, int minimumEnemyCount, int maximumEnemyCount)
	{
		this.possibleEnemies = possibleEnemies;

		this.minimumEnemyCount = minimumEnemyCount;
		this.maximumEnemyCount = maximumEnemyCount;
	}

	public EnemyEncounter CreateEncounter()
	{
		int enemyCount = Program.random.Next(minimumEnemyCount, maximumEnemyCount + 1);
		Enemy[] enemies = new Enemy[enemyCount];

		for (int i = 0; i < enemies.Length; i++)
		{
			int randomEnemyIndex = Program.random.Next(possibleEnemies.Length);
			enemies[i] = possibleEnemies[randomEnemyIndex].CreateCopy();
		}

		EnemyEncounter encounter = new EnemyEncounter(enemies, true);

		return encounter;
	}
}