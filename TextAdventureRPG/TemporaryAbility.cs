using System;
using System.Collections.Generic;

class TemporaryAbility : Ability
{
	int duration;
	List<EffectRecord> recentEffects = new List<EffectRecord>();

	// A record of a previously applied effect. This is used to undo the effect for abilities
	// that end on the user's next turn.
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

	public TemporaryAbility(string name, string menuDescription, string battleDescription, CostToUse[] costsToUse, Effect[] effects, TargetType targetType, int duration)
		: base(name, menuDescription, battleDescription, costsToUse, effects, targetType)
	{
		this.duration = duration;
	}

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

	protected override void ApplyEffectToTarget(Effect effect, Character target, Character performer, int potency, bool isOffensiveAbility)
	{
		base.ApplyEffectToTarget(effect, target, performer, potency, isOffensiveAbility);

		EffectRecord record = new EffectRecord(effect, target, performer, potency, isOffensiveAbility, duration);
		recentEffects.Add(record);
	}

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