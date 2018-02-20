using System;
using System.Collections.Generic;

/**
 * CHARACTER
 * Base class for both playable characters and enemy characters. A character is someone
 * who can participate in combat.
 */
abstract class Character
{
	public string name;
	public Pronouns pronouns;

	public int turnPriority = 0;

	public Stats baseStats;
	public Stats currentStats;

	public List<Ability> abilities = new List<Ability>();

	// Character constructor - the only required information in the base class is the character's
	// name and gender pronouns, other stats are set differently by PlayableCharacters and Enemies
	public Character(string name, Pronouns pronouns)
	{
		this.name = name;
		this.pronouns = pronouns;
	}

	// Return all of the character's stats back to their default state. This is done after battles
	// to reset heroes' HP and SP back to their maximums and to undo any stat effects.
	public void ResetStats()
	{
		currentStats = baseStats.CreateCopy();
		turnPriority = 0;
	}

	// Get the character's physical defense. Since this is a simplified RPG system, we will simply say
	// that physical defense is always equal to half of the character's strength stat.
	public int GetPhysicalDefense()
	{
		return currentStats.Get(Stats.Type.Strength) / 2;
	}

	// Get the character's magical defense. Since this is a simplified RPG system, we will simply say
	// that magical defense is always equal to half of the character's magic stat.
	public int GetMagicalDefense()
	{
		return currentStats.Get(Stats.Type.Magic) / 2;
	}

	/**
	 * STANDARD ABILITIES
	 * Defines the standard attack and defend abilities that are common across most turn-based RPGs
	 */
	public static class StandardAbilities
	{
		public static Ability attack = new Ability(
			"Attack",
			"Perform a basic physical attack against a single opponent.",
			"<PERFORMER:NAME> attacks <TARGET:NAME>!",
			null,
			new Ability.Effect[]
			{
			new Ability.Effect(
				Stats.Type.HP,
				Ability.Effect.SkillType.Physical,
				1.0f)
			},
			Ability.TargetType.SingleOpponent);
		
		public static TemporaryAbility defend = new TemporaryAbility(
			"Defend",
			"Assume a defensive stance that reduces physical and magical damage.",
			"<PERFORMER:NAME> defends <TARGET:THEMSELF>!",
			null,
			new Ability.Effect[]
			{
			new Ability.Effect(
				Stats.Type.Strength,
				Ability.Effect.SkillType.Physical,
				0.5f),
			new Ability.Effect(
				Stats.Type.Magic,
				Ability.Effect.SkillType.Magical,
				0.5f)
			},
			Ability.TargetType.SelfOnly,
			1);
	}
}