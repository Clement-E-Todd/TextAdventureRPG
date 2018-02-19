using System.Linq;

class Enemy : Character
{
	public int expReward;
	public int goldReward;

	public Enemy(string name, Pronouns pronouns, Stats baseStats, Ability[] abilities, int expReward, int goldReward) : base(name, pronouns)
	{
		this.baseStats = baseStats;
		this.abilities = abilities.ToList();
		ResetStats();

		this.expReward = expReward;
		this.goldReward = goldReward;
	}

	public Enemy CreateCopy()
	{
		Enemy enemyCopy = new Enemy(name, pronouns, baseStats, abilities.ToArray(), expReward, goldReward);
		return enemyCopy;
	}
}