using System;

/**
 * RANDOM ENCOUNTER GENERATOR
 * Used to create randomized enemy encounters. Ideally, you should create a new RandomEncounterGenerator
 * for each area that the player can travel to, each one specifying a different set of monsters that
 * can be encountered.
 */
class RandomEncounterGenerator
{
	Enemy[] possibleEnemies;

	// How many enemies can appear in an encounter?
	int minimumEnemyCount;
	int maximumEnemyCount;

	// RandomEncounterGenerator constructor - specify which enemies can appear and the minimum and maximum
	// number of enemies that can appear in a single encounter. You can add the same enemy more than once
	// to the 'possibleEnemies' array to make it more likely to appear.
	public RandomEncounterGenerator(Enemy[] possibleEnemies, int minimumEnemyCount, int maximumEnemyCount)
	{
		this.possibleEnemies = possibleEnemies;

		this.minimumEnemyCount = minimumEnemyCount;
		this.maximumEnemyCount = maximumEnemyCount;
	}

	// Generate a new EnemyEncounter used the specified parameters.
	public EnemyEncounter CreateEncounter()
	{
		int enemyCount = Program.random.Next(minimumEnemyCount, maximumEnemyCount + 1);
		Enemy[] enemies = new Enemy[enemyCount];

		// Populate the enemy party by creating copies of randomly-selected enemies from the generator's 'possibleEnemies' array
		for (int i = 0; i < enemies.Length; i++)
		{
			int randomEnemyIndex = Program.random.Next(possibleEnemies.Length);
			enemies[i] = possibleEnemies[randomEnemyIndex].CreateCopy();
		}

		// Rename enemies so we can tell them apart
		for (int i = 0; i < enemies.Length; i++)
		{
			string enemyName = enemies[i].name;
			int enemiesWithSameName = 1;

			for (int j = 0; j < enemies.Length; j++)
			{
				if (i != j && enemies[i].name == enemies[j].name)
				{
					enemiesWithSameName++;
					enemies[j].name += " " + enemiesWithSameName.ToString();
				}
			}
		}

		// Create the encounter
		EnemyEncounter encounter = new EnemyEncounter(enemies, true);

		return encounter;
	}
}