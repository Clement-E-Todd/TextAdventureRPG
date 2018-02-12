using System;

class RandomEncounterGenerator
{
	static Random random;

	Character[] possibleEnemies;
	float likelihood;
	int minimumEnemyCount;
	int maximumEnemyCount;

	public RandomEncounterGenerator(Character[] possibleEnemies, float likelihood, int minimumEnemyCount, int maximumEnemyCount)
	{
		if (random == null)
		{
			random = new Random();
		}

		this.possibleEnemies = possibleEnemies;
		this.likelihood = likelihood;
		this.minimumEnemyCount = minimumEnemyCount;
		this.maximumEnemyCount = maximumEnemyCount;
	}

	public EnemyEncounter CreateEncounter()
	{
		float randomChance = (float)random.NextDouble();
		bool startEncounter = (randomChance < likelihood);

		if (startEncounter == false)
		{
			return null;
		}

		int enemyCount = random.Next(minimumEnemyCount, maximumEnemyCount + 1);
		Character[] enemies = new Character[enemyCount];

		for (int i = 0; i < enemies.Length; i++)
		{
			int randomEnemyIndex = random.Next(possibleEnemies.Length);
			enemies[i] = possibleEnemies[randomEnemyIndex].CreateCopy();
		}

		EnemyEncounter encounter = new EnemyEncounter(enemies);

		return encounter;
	}
}