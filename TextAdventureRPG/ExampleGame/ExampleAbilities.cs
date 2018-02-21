
/**
 * EXAMPLE ABILITIES
 * A static class which defines all of the abilities used in the example game (except for
 * the standard attack and defend abilities - those are defined in Character.cs).
 */
static class ExampleAbilities
{
	// ACID
	public static Ability acid = new Ability(
		"Acid Spray",
		"Summon a spray of acid to burn all opponents.",
		"<PERFORMER:NAME> sprays a blast of acid!",
		new Ability.CostToUse[]
		{
			new Ability.CostToUse(Stats.Type.SP, 25)
		},
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.HP,
				Ability.Effect.SkillType.Magical,
				1.2f)
		},
		Ability.TargetType.AllOpponents);

	// BITE
	public static Ability bite = new Ability(
		"Bite",
		"Sink your teeth into a single opponent.",
		"<PERFORMER:NAME> bites <TARGET:NAME>!",
		null,
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.HP,
				Ability.Effect.SkillType.Physical,
				1.0f)
		},
		Ability.TargetType.SingleOpponent);

	// CLAW
	public static Ability claw = new Ability(
		"Claw",
		"Slash at a single opponent with your claws.",
		"<PERFORMER:NAME> lashes at <TARGET:NAME> with <PERFORMER:THEIR> claws!",
		null,
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.HP,
				Ability.Effect.SkillType.Physical,
				1.1f)
		},
		Ability.TargetType.SingleOpponent);

	// CREEPY CRAWL
	public static TemporaryAbility creepyCrawl = new TemporaryAbility(
		"Creepy Crawl",
		"Crawl unsettlingly up an opponent's legs to distract them.",
		"<PERFORMER:NAME> scuttles up <TARGET:NAME>'s leg!",
		null,
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.Strength,
				Ability.Effect.SkillType.Physical,
				1f),
			new Ability.Effect(
				Stats.Type.Magic,
				Ability.Effect.SkillType.Physical,
				1f),
			new Ability.Effect(
				Stats.Type.Speed,
				Ability.Effect.SkillType.Physical,
				1f)
		},
		Ability.TargetType.SingleOpponent,
		2);

	// DIVE
	public static TemporaryAbility dive = new TemporaryAbility(
		"Dive",
		"Fly high and dive to achieve blinding speed!",
		"<PERFORMER:NAME> dives from high in the sky, <PERFORMER:THEIR> movement is a blur!",
		new Ability.CostToUse[]
		{
			new Ability.CostToUse(Stats.Type.SP, 15)
		},
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.Speed,
				Ability.Effect.SkillType.Physical,
				1f)
		},
		Ability.TargetType.SelfOnly,
		2);

	// ENERGY SHOWER
	public static Ability energyShower = new Ability(
		"Energy Shower",
		"Transforms a portion of your life energy into SP for your entire party.",
		"<PERFORMER:NAME> closes <PERFORMER:THEIR> eyes and focuses...\nEnergy visibly flows from <PERFORMER:THEIR> body!",
		new Ability.CostToUse[]
		{
			new Ability.CostToUse(Stats.Type.HP, 15)
		},
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.SP,
				Ability.Effect.SkillType.Magical,
				1f)
		},
		Ability.TargetType.AllAllies);

	// FIRE BREATH
	public static Ability fireBreath = new Ability(
		"Fire Breath",
		"Bellow forth a massive breath of flames, incinerating all opponents before you!",
		"<PERFORMER:NAME> unleashes a massive blast of flaming breath!",
		new Ability.CostToUse[]
		{
			new Ability.CostToUse(Stats.Type.SP, 30),
			new Ability.CostToUse(Stats.Type.HP, 10)
		},
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.HP,
				Ability.Effect.SkillType.Magical,
				3f)
		},
		Ability.TargetType.AllOpponents);

	// FOCUS
	public static Ability focus = new Ability(
		"Focus",
		"Transforms a small amount of your life energy into SP.",
		"<PERFORMER:NAME> closes <PERFORMER:THEIR> eyes and focuses...",
		new Ability.CostToUse[]
		{
			new Ability.CostToUse(Stats.Type.HP, 5)
		},
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.SP,
				Ability.Effect.SkillType.Magical,
				1f)
		},
		Ability.TargetType.SelfOnly);

	// GLARE
	public static TemporaryAbility glare = new TemporaryAbility(
		"Glare",
		"Glare at a single opponent with a petrifying gaze.",
		"<PERFORMER:NAME> glares piercingly at <TARGET:NAME>!",
		new Ability.CostToUse[]
		{
			new Ability.CostToUse(Stats.Type.SP, 20)
		},
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.Speed,
				Ability.Effect.SkillType.Magical,
				2f)
		},
		Ability.TargetType.SingleAlly,
		1);

	// HEAL
	public static Ability heal = new Ability(
		"Heal",
		"Cast a soothing aura to recover one ally's HP.",
		"<PERFORMER:NAME> casts a soothing aura on <TARGET:NAME>!",
		new Ability.CostToUse[]
		{
			new Ability.CostToUse(Stats.Type.SP, 10)
		},
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.HP,
				Ability.Effect.SkillType.Magical,
				0.25f)
		},
		Ability.TargetType.SingleAlly);

	// HEAL ALL
	public static Ability healAll = new Ability(
		"Heal All",
		"Cast a soothing aura to recover all allies' HP.",
		"<PERFORMER:NAME> casts a soothing aura on <PERFORMER:THEIR> team!",
		new Ability.CostToUse[]
		{
			new Ability.CostToUse(Stats.Type.SP, 30)
		},
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.HP,
				Ability.Effect.SkillType.Magical,
				0.25f)
		},
		Ability.TargetType.AllAllies);

	// HEAVY ATTACK
	public static Ability heavyAttack = new Ability(
		"Heavy Attack",
		"Put your weight into your attack to deal extra damage.",
		"<PERFORMER:NAME> deals a heavy blow to <TARGET:NAME>!",
		new Ability.CostToUse[]
		{
			new Ability.CostToUse(Stats.Type.SP, 5),
			new Ability.CostToUse(Stats.Type.HP, 5)
		},
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.HP,
				Ability.Effect.SkillType.Physical,
				1.25f)
		},
		Ability.TargetType.SingleOpponent);

	// JIGGLE
	public static Ability jiggle = new Ability(
		"Jiggle",
		"Jiggle rapidly, absorbing mana from the environment.",
		"<PERFORMER:NAME> jiggles threateningly!",
		null,
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.SP,
				Ability.Effect.SkillType.Physical,
				0.5f)
		},
		Ability.TargetType.SelfOnly);
	
	// MAGIC BLAST
	public static Ability magicBlast = new Ability(
		"Acid Spray",
		"Summon a blast of magic to damage all opponents.",
		"<PERFORMER:NAME> conjures a mighty magic blast!",
		new Ability.CostToUse[]
		{
			new Ability.CostToUse(Stats.Type.SP, 30)
		},
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.HP,
				Ability.Effect.SkillType.Magical,
				1.25f)
		},
		Ability.TargetType.AllOpponents);

	// SPIN ATTACK
	public static Ability spinAttack = new Ability(
		"Spin Attack",
		"Spin around to hit all opponents during your attack",
		"<PERFORMER:NAME> attacks in a wide, spinning arc!",
		new Ability.CostToUse[]
		{
			new Ability.CostToUse(Stats.Type.SP, 10),
			new Ability.CostToUse(Stats.Type.HP, 5)
		},
		new Ability.Effect[]
		{
			new Ability.Effect(
				Stats.Type.HP,
				Ability.Effect.SkillType.Physical,
				1.0f)
		},
		Ability.TargetType.AllOpponents);

	// SUPPORT ALLY
	public static TemporaryAbility supportAlly = new TemporaryAbility(
		"Support Ally",
		"Provide backup for a single ally, allowing them improved physical and magical abilities.",
		"<PERFORMER:NAME> lends <TARGET:NAME> <PERFORMER:THEIR> strength!",
		new Ability.CostToUse[]
		{
			new Ability.CostToUse(Stats.Type.SP, 10),
			new Ability.CostToUse(Stats.Type.HP, 25)
		},
		new Ability.Effect[]
		{
		new Ability.Effect(
			Stats.Type.Strength,
			Ability.Effect.SkillType.Physical,
			0.6f),
		new Ability.Effect(
			Stats.Type.Magic,
			Ability.Effect.SkillType.Magical,
			0.6f)
		},
		Ability.TargetType.SingleAlly,
		2);

	/**
	* ENEMY MOVESETS
	* A static class which defines arrays of moves for each enemy in the example game.
	*/
	public static class EnemyMovesets
	{
		public static Ability[] rat = new Ability[]
		{
			bite,
			Character.StandardAbilities.defend
		};

		public static Ability[] spider = new Ability[]
		{
			bite,
			creepyCrawl
		};

		public static Ability[] slime = new Ability[]
		{
			Character.StandardAbilities.attack,
			acid,
			jiggle
		};

		public static Ability[] orc = new Ability[]
		{
			Character.StandardAbilities.attack,
			Character.StandardAbilities.defend,
			heavyAttack,
			heavyAttack,
			spinAttack,
			spinAttack
		};

		public static Ability[] hellhound = new Ability[]
		{
			bite,
			claw
		};

		public static Ability[] gorgon = new Ability[]
		{
			Character.StandardAbilities.attack,
			glare,
			glare,
			focus
		};

		public static Ability[] dragon = new Ability[]
		{
			bite,
			claw,
			heavyAttack,
			fireBreath
		};

		public static Ability[] harpy = new Ability[]
		{
			claw,
			claw,
			spinAttack,
			dive
		};

		public static Ability[] lich = new Ability[]
		{
			Character.StandardAbilities.attack,
			heal,
			healAll,
			magicBlast,
			energyShower
		};
	}
}