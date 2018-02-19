static class EnemyDatabase
{
	// Enemies that appear in the example game...
	public static class Example
	{
		// Beginner-tier enemies
		public static Enemy rat = new Enemy("Rat", Pronouns.Presets.it, new Stats(30, 0, 15, 0, 10), AbilityDatabase.Example.EnemyMovesets.rat);
		public static Enemy spider = new Enemy("Spider", Pronouns.Presets.it, new Stats(25, 0, 10, 0, 15), AbilityDatabase.Example.EnemyMovesets.spider);
		public static Enemy slime = new Enemy("Slime", Pronouns.Presets.it, new Stats(25, 50, 10, 15, 10), AbilityDatabase.Example.EnemyMovesets.slime);
	}
}