static class AbilityDatabase
{
	/**
	 * BASIC PHYSICAL ATTACK
	 */
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

	/**
	 * BASIC DEFENSIVE TECHNIQUE
	 */
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

	/**
	 * ABILITIES FROM THE EXAMPLE GAME
	 */
	public static class Example
	{
		// ACID
		public static Ability acid = new Ability(
			"Acid Spray",
			"Summon a spray of acid to burn all opponents.",
			"<PERFORMER:NAME> sprays a blast of acid!",
			new Ability.CostToUse[]
			{
				new Ability.CostToUse(Ability.CostToUse.Type.SP, 20)
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
					0.9f),
				new Ability.Effect(
					Stats.Type.Magic,
					Ability.Effect.SkillType.Physical,
					0.9f),
				new Ability.Effect(
					Stats.Type.Speed,
					Ability.Effect.SkillType.Physical,
					0.9f)
			},
			Ability.TargetType.SingleOpponent,
			2);

		// HEAVY ATTACK
		public static Ability heavyAttack = new Ability(
			"Heavy Attack",
			"Put your weight into your attack to deal extra damage.",
			"<PERFORMER:NAME> deals a heavy blow to <TARGET:NAME>!",
			new Ability.CostToUse[]
			{
				new Ability.CostToUse(Ability.CostToUse.Type.SP, 5),
				new Ability.CostToUse(Ability.CostToUse.Type.HP, 5)
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

		// SOOTHE
		public static Ability soothe = new Ability(
			"Soothe",
			"Cast a gentle healing aura to slightly recover one ally's HP.",
			"<PERFORMER:NAME> casts a soothing aura on <TARGET:NAME>!",
			new Ability.CostToUse[]
			{
				new Ability.CostToUse(Ability.CostToUse.Type.SP, 10)
			},
			new Ability.Effect[]
			{
				new Ability.Effect(
					Stats.Type.HP,
					Ability.Effect.SkillType.Magical,
					0.25f)
			},
			Ability.TargetType.SingleAlly);

		/**
		* ENEMY ABILITY LISTS
		*/
		public static class EnemyMovesets
		{
			public static Ability[] rat = new Ability[]
			{
				bite,
				defend
			};

			public static Ability[] spider = new Ability[]
			{
				bite,
				creepyCrawl
			};

			public static Ability[] slime = new Ability[]
			{
				attack,
				acid,
				jiggle
			};
		}
	}
}