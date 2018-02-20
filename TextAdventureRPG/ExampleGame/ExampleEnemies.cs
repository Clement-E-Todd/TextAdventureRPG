/**
 * EXAMPLE ENEMIES
 * A static class that contains the templates for the enemies that appear in the example game.
 */
static class ExampleEnemies
{
	// Beginner-tier enemies
	public static Enemy rat = new Enemy("Rat", Pronouns.Presets.it, new Stats(30, 0, 15, 0, 8), ExampleAbilities.EnemyMovesets.rat, 2, 15);
	public static Enemy spider = new Enemy("Spider", Pronouns.Presets.it, new Stats(25, 0, 12, 5, 15), ExampleAbilities.EnemyMovesets.spider, 2, 5);
	public static Enemy slime = new Enemy("Slime", Pronouns.Presets.it, new Stats(25, 50, 11, 15, 10), ExampleAbilities.EnemyMovesets.slime, 3, 10);

	// Intermediate-tier enemies
	public static Enemy orc = new Enemy("Orc", Pronouns.Presets.masculine, new Stats(70, 15, 30, 5, 16), ExampleAbilities.EnemyMovesets.orc, 10, 75);
	public static Enemy hellhound = new Enemy("Hellhound", Pronouns.Presets.it, new Stats(60, 15, 25, 10, 30), ExampleAbilities.EnemyMovesets.hellhound, 10, 25);
	public static Enemy gorgon = new Enemy("Gorgon", Pronouns.Presets.feminine, new Stats(50, 100, 20, 30, 20), ExampleAbilities.EnemyMovesets.gorgon, 10, 50);

	// Advanced-tier enemies
	public static Enemy dragon = new Enemy("Dragon", Pronouns.Presets.it, new Stats(140, 30, 60, 10, 40), ExampleAbilities.EnemyMovesets.dragon, 30, 150);
	public static Enemy harpy = new Enemy("Harpy", Pronouns.Presets.feminine, new Stats(120, 30, 50, 20, 70), ExampleAbilities.EnemyMovesets.harpy, 25, 125);
	public static Enemy lich = new Enemy("Lich", Pronouns.Presets.masculine, new Stats(100, 200, 40, 60, 40), ExampleAbilities.EnemyMovesets.lich, 25, 250);
}