﻿using System;
using System.Collections.Generic;
using System.Threading;

/**
 * ABILITY
 * An ability is any action that a character (hero or enemy) can perform in battle.
 * Each character has their own personal list of abilities that they can perform.
 */
class Ability
{
	public string name;
	public string menuDescription;
	private string battleDescription;

	public CostToUse[] costsToUse;
	public Effect[] effects;
	public TargetType targetType;

	// The "CostToUse" struct describes which resource the character will have to
	// spend to use the ability (HP or SP) and how much.
	public struct CostToUse
	{
		public Stats.Type type;
		public int amount;

		public CostToUse(Stats.Type type, int amount)
		{
			this.type = type;
			this.amount = amount;
		}
	}

	// The "Effect" struct describes what kind of effect the ability will have on its target
	public struct Effect
	{
		// Describes whether the ability is physical or magical in nature
		public enum SkillType
		{
			Physical,
			Magical
		}
		
		// Which stat is changed by the effect
		public Stats.Type stat;

		// Physical or Magical?
		public SkillType skillType;

		// Increases the effectiveness of the effect based on the user's strength or magic (depending on whether
		// the ability is physical or magical).
		public float multiplier;

		public Effect(Stats.Type stat, SkillType skillType, float multiplier)
		{
			this.stat = stat;
			this.skillType = skillType;
			this.multiplier = multiplier;
		}
	}

	// Who the target of the ability is
	public enum TargetType
	{
		SingleOpponent,
		AllOpponents,
		SingleAlly,
		AllAllies,
		SelfOnly
	}

	// Ability constructor - all of the ability's variables are initialized here.
	public Ability(string name, string menuDescription, string battleDescription, CostToUse[] costsToUse, Effect[] effects, TargetType targetType)
	{
		this.name = name;
		this.menuDescription = menuDescription;
		this.battleDescription = battleDescription;

		this.costsToUse = costsToUse;
		this.effects = effects;
		this.targetType = targetType;
	}

	// This function is used to execute an ability by selecting a target and applying the ability's effects to it.
	public void Perform(Character performer, Character[] targetParty)
	{
		List<Character> targetsAffected = new List<Character>();

		// Defeated characters can't be targeted, so filter them out of the target party
		List<Character> validTargets = new List<Character>();
		if (targetParty != null)
		{
			foreach (Character character in targetParty)
			{
				if (character.currentStats.Get(Stats.Type.HP) > 0)
				{
					validTargets.Add(character);
				}
			}
		}

		// If this ability targets the entire party, simply set targetsAffected to targetParty
		if (targetType == TargetType.AllAllies || targetType == TargetType.AllOpponents)
		{
			targetsAffected = validTargets;
		}

		// If this ability targets one character (and is not self-only), select the target from the target party
		else if (targetType == TargetType.SingleAlly || targetType == TargetType.SingleOpponent)
		{
			// If the performer is a player character, allow the player to choose the target.
			// Otherwise, pick randomly.
			if (performer is PlayerCharacter)
			{
				Console.WriteLine("Who will " + performer.pronouns.they + " target?");
				for (int i = 0; i < validTargets.Count; i++)
				{
					Console.WriteLine("[" + (i + 1) + "] " + validTargets[i].name);
				}

				while (targetsAffected.Count == 0)
				{
					string userInput = Console.ReadLine().ToUpper();

					int numericInput;
					bool isInputNumeric = int.TryParse(userInput, out numericInput);

					if (isInputNumeric && numericInput >= 1 && numericInput <= validTargets.Count)
					{
						targetsAffected.Add(validTargets[numericInput - 1]);
					}
					else
					{
						Console.WriteLine("Invalid input.");
					}
				}
			}
			else
			{
				targetsAffected.Add(validTargets[Program.random.Next(validTargets.Count)]);
			}
		}

		// If the ability can only target the character who uses it, simply add the performer to the affected targets
		else if (targetType == TargetType.SelfOnly)
		{
			targetsAffected.Add(performer);
		}

		// Write out the actions that are transpiring
		if (targetsAffected.Count == 1)
		{
			WriteBattleDescription(performer, targetsAffected[0]);
		}
		else
		{
			WriteBattleDescription(performer);
		}

		// Pause for 1 second (1000 milliseconds) for dramatic effect
		Thread.Sleep(1000);

		// Apply the ability's effect to each of the targets
		foreach (Character target in targetsAffected)
		{
			// Loop over each of this ability's effects and apply it to the target
			foreach (Effect effect in effects)
			{
				// Check whether this ability is offensive or supportive so that we know whether to add or subtract
				// from the target's stats
				bool isOffensiveAbility = (targetType == TargetType.SingleOpponent || targetType == TargetType.AllOpponents);

				// Calculate how effective the ability is against the target by comparing the performer and target's stats
				int potency = CalculateEffectPotency(effect, target, performer, isOffensiveAbility);

				// Write out a description of the effect
				WriteStatChangeDescription(target.name, effect.stat, potency, isOffensiveAbility);

				// Apply the effect with the calculated potency to the target
				ApplyEffectToTarget(effect, target, performer, potency, isOffensiveAbility);

				// Pause briefly between displaying each effect
				Thread.Sleep(500);
			}
		}

		// Reduce the performer's SP and / or HP based on the abilit's cost
		if (costsToUse != null)
		{
			foreach (CostToUse cost in costsToUse)
			{
				performer.currentStats.Subtract(cost.type, cost.amount);
			}
		}
	}

	// Get the amount that this ability should affect the target, based on the performer's strength if the
	// effect is physical or based on the performer's magic if the effect is magical.
	private int CalculateEffectPotency(Effect effect, Character target, Character performer, bool isOffensiveAbility)
	{
		int potency = 0;

		if (effect.skillType == Effect.SkillType.Physical)
		{
			potency = (int)(performer.currentStats.Get(Stats.Type.Strength) * effect.multiplier);
		}
		else if (effect.skillType == Effect.SkillType.Magical)
		{
			potency = (int)(performer.currentStats.Get(Stats.Type.Magic) * effect.multiplier);
		}

		// If this is an offensive ability, reduce how much the target is affected based on their defense.
		if (isOffensiveAbility)
		{
			int defenseAmount = 0;

			if (effect.skillType == Effect.SkillType.Physical)
			{
				defenseAmount = target.GetPhysicalDefense();
			}
			else if (effect.skillType == Effect.SkillType.Magical)
			{
				defenseAmount = target.GetMagicalDefense();
			}

			potency -= defenseAmount;

			if (potency < 0)
			{
				potency = 0;
			}
		}

		return potency;
	}

	// Raise or lower a target's stats based on the calculations of CalculateEffectPotency (see above)
	protected virtual void ApplyEffectToTarget(Effect effect, Character target, Character performer, int potency, bool isOffensiveAbility)
	{
		if (isOffensiveAbility)
		{
			target.currentStats.Subtract(effect.stat, potency);
		}
		else
		{
			target.currentStats.Add(effect.stat, potency);

			// Don't allow the character's HP and SP to exceed their maximum values
			if (effect.stat == Stats.Type.HP && target.currentStats.Get(Stats.Type.HP) > target.baseStats.Get(Stats.Type.HP))
			{
				target.currentStats.Set(Stats.Type.HP, target.baseStats.Get(Stats.Type.HP));
			}
			else if (effect.stat == Stats.Type.SP && target.currentStats.Get(Stats.Type.SP) > target.baseStats.Get(Stats.Type.SP))
			{
				target.currentStats.Set(Stats.Type.SP, target.baseStats.Get(Stats.Type.SP));
			}
		}
	}

	// Check a character's SP and HP to see whether or not they are able to use this move currently
	public bool CanCharacterAffordCosts(Character character)
	{
		if (costsToUse == null)
		{
			return true;
		}

		bool canUseAbility = true;

		for (int j = 0; j < costsToUse.Length; j++)
		{
			if ((costsToUse[j].type == Stats.Type.HP && character.currentStats.Get(Stats.Type.HP) <= costsToUse[j].amount) ||
				(costsToUse[j].type == Stats.Type.SP && character.currentStats.Get(Stats.Type.SP) < costsToUse[j].amount))
			{
				canUseAbility = false;
				break;
			}
		}

		return canUseAbility;
	}

	// Insert the characters' names and pronouns into the description of the action that appears during battle,
	// then write that description out to the console.
	private void WriteBattleDescription(Character performer, Character target = null)
	{
		string formattedText = battleDescription;

		formattedText = formattedText.Replace("<PERFORMER:NAME>", performer.name);
		formattedText = formattedText.Replace("<PERFORMER:THEY>", performer.pronouns.they);
		formattedText = formattedText.Replace("<PERFORMER:THEM>", performer.pronouns.them);
		formattedText = formattedText.Replace("<PERFORMER:THEIR>", performer.pronouns.their);
		formattedText = formattedText.Replace("<PERFORMER:THEIRS>", performer.pronouns.theirs);
		formattedText = formattedText.Replace("<PERFORMER:THEMSELF>", performer.pronouns.themself);

		if (target != null)
		{
			if (target != performer)
			{
				formattedText = formattedText.Replace("<TARGET:NAME>", target.name);
				formattedText = formattedText.Replace("<TARGET:THEY>", target.pronouns.they);
				formattedText = formattedText.Replace("<TARGET:THEM>", target.pronouns.them);
				formattedText = formattedText.Replace("<TARGET:THEIR>", target.pronouns.their);
				formattedText = formattedText.Replace("<TARGET:THEIRS>", target.pronouns.theirs);
				formattedText = formattedText.Replace("<TARGET:THEMSELF>", target.pronouns.themself);
			}
			else
			{
				formattedText = formattedText.Replace("<TARGET:NAME>", performer.pronouns.themself);
				formattedText = formattedText.Replace("<TARGET:THEY>", performer.pronouns.they);
				formattedText = formattedText.Replace("<TARGET:THEM>", performer.pronouns.themself);
				formattedText = formattedText.Replace("<TARGET:THEIR>", performer.pronouns.their);
				formattedText = formattedText.Replace("<TARGET:THEIRS>", performer.pronouns.theirs);
				formattedText = formattedText.Replace("<TARGET:THEMSELF>", performer.pronouns.themself);
			}
		}

		Console.WriteLine(formattedText);
	}

	// Describe the change in a target's stats in the console (ie, "Spider's HP falls by 10!")
	protected virtual void WriteStatChangeDescription(string targetName, Stats.Type stat, int amountToApply, bool isOffensiveAbility)
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

		Console.WriteLine(targetName + "'s " + stat + " " + verb + " by " + amountToApply + "!");
	}

	// Get a string that describes how much it costs to use this ability (ie "(Costs 10 SP and 5 HP)")
	public string GetCostDescription()
	{
		if (costsToUse != null && costsToUse.Length > 0)
		{
			string abilityMessage = "(Costs ";

			for (int j = 0; j < costsToUse.Length; j++)
			{
				abilityMessage += costsToUse[j].amount + " " + costsToUse[j].type;

				if (j < costsToUse.Length - 1)
				{
					abilityMessage += " and ";
				}
			}
			abilityMessage += " to use)";

			return abilityMessage;
		}

		return null;
	}
}