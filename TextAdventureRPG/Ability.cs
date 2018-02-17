using System;
using System.Threading;

class Ability
{
	public string name;
	public string menuDescription;
	private string battleDescription;

	public CostToUse[] costsToUse;
	public Effect[] effects;
	public TargetType targetType;

	public struct CostToUse
	{
		public enum Type
		{
			HP,
			SP
		}

		public Type type;
		public int amount;

		public CostToUse(Type type, int amount)
		{
			this.type = type;
			this.amount = amount;
		}
	}

	public struct Effect
	{
		public enum TargetedStat
		{
			HP,
			SP,
			Strength,
			Magic,
			Speed
		}

		public enum SkillType
		{
			Physical,
			Magical
		}

		public enum Duration
		{
			Immediate,
			UntilNextTurn
		}

		public TargetedStat stat;
		public SkillType skillType;
		public Duration duration;
		public float multiplier;

		public Effect(TargetedStat stat, SkillType skillType, Duration duration, float multiplier)
		{
			this.stat = stat;
			this.skillType = skillType;
			this.duration = duration;
			this.multiplier = multiplier;
		}
	}

	public enum TargetType
	{
		SingleOpponent,
		AllOpponents,
		SingleAlly,
		AllAllies,
		SelfOnly
	}

	public Ability(string name, string menuDescription, string battleDescription, CostToUse[] costsToUse, Effect[] effects, TargetType targetType)
	{
		this.name = name;
		this.menuDescription = menuDescription;
		this.battleDescription = battleDescription;

		this.costsToUse = costsToUse;
		this.effects = effects;
		this.targetType = targetType;
	}

	public void Perform(Character performer, Character[] targetParty)
	{
		Character[] targetsAffected = null;

		// If this ability targets the entire party, simply set targetsAffected to targetParty
		if (targetType == TargetType.AllAllies || targetType == TargetType.AllOpponents)
		{
			targetsAffected = targetParty;
		}

		// If this ability targets one character (and is not self-only), select the target from the target party
		else if (targetType == TargetType.SingleAlly || targetType == TargetType.SingleOpponent)
		{
			targetsAffected = new Character[1];

			// If the performer is a player character, allow the player to choose the target.
			// Otherwise, pick randomly.
			if (performer is PlayerCharacter)
			{
				Console.WriteLine("Who will " + performer.pronouns.they + " target?");
				for (int i = 0; i < targetParty.Length; i++)
				{
					Console.WriteLine("[" + (i + 1) + "] " + targetParty[i].name);
				}
				
				while (targetsAffected[0] == null)
				{
					string userInput = Console.ReadLine().ToUpper();

					int numericInput;
					bool isInputNumeric = int.TryParse(userInput, out numericInput);

					if (isInputNumeric && numericInput >= 1 && numericInput <= targetParty.Length)
					{
						targetsAffected[0] = targetParty[numericInput - 1];
					}
					else
					{
						Console.WriteLine("Invalid input.");
					}
				}
			}
			else
			{
				targetsAffected[0] = targetParty[Program.random.Next(targetParty.Length)];
			}
		}

		// Write out the actions that are transpiring
		if (targetsAffected.Length == 1)
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
		foreach (Character target in targetParty)
		{
			bool isOffensiveAbility = (targetType == TargetType.SingleOpponent || targetType == TargetType.AllOpponents);

			// Loop over each of this ability's effects and apply it to the target
			foreach (Effect effect in effects)
			{
				// Get the amount that this ability should affect the target, based on the performer's strength if the
				// effect is physical or based on the performer's magic if the effect is magical.
				int amountToApply = 0;

				if (effect.skillType == Effect.SkillType.Physical)
				{
					amountToApply = (int)(performer.currentStats.strength * effect.multiplier);
				}
				else if (effect.skillType == Effect.SkillType.Magical)
				{
					amountToApply = (int)(performer.currentStats.magic * effect.multiplier);
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

					amountToApply -= defenseAmount;

					if (amountToApply < 0)
					{
						amountToApply = 0;
					}
				}


			}
		}
	}

	private void WriteBattleDescription(Character performer, Character target = null)
	{
		string formattedText = battleDescription;

		formattedText = formattedText.Replace("PERFORMER:NAME", performer.name);
		formattedText = formattedText.Replace("PERFORMER:THEY", performer.pronouns.they);
		formattedText = formattedText.Replace("PERFORMER:THEM", performer.pronouns.them);
		formattedText = formattedText.Replace("PERFORMER:THEIR", performer.pronouns.their);
		formattedText = formattedText.Replace("PERFORMER:THEIRS", performer.pronouns.theirs);
		formattedText = formattedText.Replace("PERFORMER:THEMSELF", performer.pronouns.themself);

		if (target != null)
		{
			if (target != performer)
			{
				formattedText = formattedText.Replace("TARGET:NAME", target.name);
				formattedText = formattedText.Replace("TARGET:THEY", target.pronouns.they);
				formattedText = formattedText.Replace("TARGET:THEM", target.pronouns.them);
				formattedText = formattedText.Replace("TARGET:THEIR", target.pronouns.their);
				formattedText = formattedText.Replace("TARGET:THEIRS", target.pronouns.theirs);
				formattedText = formattedText.Replace("TARGET:THEMSELF", target.pronouns.themself);
			}
			else
			{
				formattedText = formattedText.Replace("TARGET:NAME", performer.pronouns.themself);
				formattedText = formattedText.Replace("TARGET:THEY", performer.pronouns.they);
				formattedText = formattedText.Replace("TARGET:THEM", performer.pronouns.themself);
				formattedText = formattedText.Replace("TARGET:THEIR", performer.pronouns.their);
				formattedText = formattedText.Replace("TARGET:THEIRS", performer.pronouns.theirs);
				formattedText = formattedText.Replace("TARGET:THEMSELF", performer.pronouns.themself);
			}
		}

		Console.WriteLine(formattedText);
	}
}