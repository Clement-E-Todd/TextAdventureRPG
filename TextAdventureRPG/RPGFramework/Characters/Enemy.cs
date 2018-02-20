using System.Linq;

/**
 * ENEMY
 * Represents an opponent to the player's party in battle.
 */
class Enemy : Character
{
	public int expReward;
	public int goldReward;

	// Enemy constructor - Specifies the enemy's stats, abilities and rewards when they defeated.
	public Enemy(string name, Pronouns pronouns, Stats baseStats, Ability[] abilities, int expReward, int goldReward) : base(name, pronouns)
	{
		this.baseStats = baseStats;
		this.abilities = abilities.ToList();
		ResetStats();

		this.expReward = expReward;
		this.goldReward = goldReward;
	}

	// Create a copy of the enemy. This is used by the random encounter generator to create multiple copies
	// of the same monster in a single battle.
	public Enemy CreateCopy()
	{
		Enemy enemyCopy = new Enemy(name, pronouns, baseStats, abilities.ToArray(), expReward, goldReward);
		return enemyCopy;
	}
}