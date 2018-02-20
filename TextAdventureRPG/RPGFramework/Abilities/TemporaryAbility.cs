using System;
using System.Collections.Generic;

/**
 * TEMPORARY ABILITY
 * A temporary ability is an ability whose effects only last for a set number of turns. A common example of this
 * would be the "defend" ability, which raises the user's stats to protect them from damage but expires at the
 * beginning of the user's next turn.
 */
class TemporaryAbility : Ability
{
	int duration;
	List<EffectRecord> recentEffects = new List<EffectRecord>();

	/**
	 * EFFECT RECORD
	 * A record of a previously applied effect. This is used to undo the effects of an ability after it expires.
	 */
	class EffectRecord
	{
		public Effect effect;
		public Character target;
		public Character performer;
		public int potency;
		public bool isOffensiveAbility;
		public int turnsRemaining;

		public EffectRecord(Effect effect, Character target, Character performer, int potency, bool isOffensiveAbility, int duration)
		{
			this.effect = effect;
			this.target = target;
			this.performer = performer;
			this.potency = potency;
			this.isOffensiveAbility = isOffensiveAbility;

			turnsRemaining = duration;
		}
	}

	// TemporaryAbility constructor - The same as the Ability base class's constructor, but also includes the number of turns
	// until the ability's effect expire.
	public TemporaryAbility(string name, string menuDescription, string battleDescription, CostToUse[] costsToUse, Effect[] effects, TargetType targetType, int duration)
		: base(name, menuDescription, battleDescription, costsToUse, effects, targetType)
	{
		this.duration = duration;
	}

	// Called at the beginning of a character's turn, this function counts down the number of turns until the effect's expiry.
	public void OnCharacterTurnStart(Character character)
	{
		// Loop backwards through the list of recent effects and remove them if they've expired. We have to loop backwards
		// to prevent the value of i from becoming incorrect if we remove something from the middle of the list.
		for (int i = recentEffects.Count - 1; i >= 0; i--)
		{
			if (recentEffects[i].performer == character)
			{
				recentEffects[i].turnsRemaining -= 1;

				if (recentEffects[i].turnsRemaining <= 0)
				{
					UndoEffect(recentEffects[i]);
					recentEffects.RemoveAt(i);
				}
			}
		}
	}

	// Extends the base Ability class's ApplyEffectToTarget function. A temporary ability records the applied effects so
	// that it can undo them later when they expire.
	protected override void ApplyEffectToTarget(Effect effect, Character target, Character performer, int potency, bool isOffensiveAbility)
	{
		base.ApplyEffectToTarget(effect, target, performer, potency, isOffensiveAbility);

		EffectRecord record = new EffectRecord(effect, target, performer, potency, isOffensiveAbility, duration);
		recentEffects.Add(record);
	}

	// Revert an effect, reversing the original change in the target's stats.
	private void UndoEffect(EffectRecord record)
	{
		if (record.isOffensiveAbility)
		{
			record.target.currentStats.Add(record.effect.stat, record.potency);

			// Don't allow the character's HP and SP to exceed their maximum values
			if (record.effect.stat == Stats.Type.HP && record.target.currentStats.Get(Stats.Type.HP) > record.target.baseStats.Get(Stats.Type.HP))
			{
				record.target.currentStats.Set(Stats.Type.HP, record.target.baseStats.Get(Stats.Type.HP));
			}
			else if (record.effect.stat == Stats.Type.SP && record.target.currentStats.Get(Stats.Type.SP) > record.target.baseStats.Get(Stats.Type.SP))
			{
				record.target.currentStats.Set(Stats.Type.SP, record.target.baseStats.Get(Stats.Type.SP));
			}
		}
		else
		{
			record.target.currentStats.Subtract(record.effect.stat, record.potency);
		}
	}

	// Override the base Ability class's stat change description to indicate that the change is only temporary.
	protected override void WriteStatChangeDescription(string targetName, Stats.Type stat, int amountToApply, bool isOffensiveAbility)
	{
		string verb = "";

		if (isOffensiveAbility)
		{
			verb = "falls";
		}
		else
		{
			verb = "rises";
		}

		Console.WriteLine(targetName + "'s " + stat + " " + verb + " by " + amountToApply + " temporarily!");
	}
}