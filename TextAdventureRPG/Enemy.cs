using System.Linq;

class Enemy : Character
{
	public Enemy(string name, Pronouns pronouns, Stats baseStats, Ability[] abilities) : base(name, pronouns)
	{
		this.baseStats = baseStats;
		this.abilities = abilities.ToList();
		ResetStats();
	}

	public Enemy CreateCopy()
	{
		Enemy enemyCopy = new Enemy(name, pronouns, baseStats, abilities.ToArray());
		return enemyCopy;
	}
}