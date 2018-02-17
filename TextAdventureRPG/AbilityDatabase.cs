static class AbilityDatabase
{
	/**
	 * BASIC PHYSICAL ATTACK
	 */
	public static Ability attack = new Ability(
		"Attack",
		"Perform a basic physical attack against a single opponent.",
		"PERFORMER:NAME attacks TARGET:NAME!",
		null,
		new Ability.Effect[]
		{
			new Ability.Effect(
				Ability.Effect.TargetedStat.HP,
				Ability.Effect.SkillType.Physical,
				Ability.Effect.Duration.Immediate,
				1.0f)
		},
		Ability.TargetType.SingleOpponent);

	/**
	 * BASIC DEFENSIVE TECHNIQUE
	 */
	public static Ability defend = new Ability(
		"Defend",
		"Assume a defensive stance that reduces physical and magical damage.",
		"PERFORMER:NAME defends TARGET:THEMSELF!",
		null,
		new Ability.Effect[]
		{
			new Ability.Effect(
				Ability.Effect.TargetedStat.Strength,
				Ability.Effect.SkillType.Physical,
				Ability.Effect.Duration.UntilNextTurn,
				0.5f),
			new Ability.Effect(
				Ability.Effect.TargetedStat.Magic,
				Ability.Effect.SkillType.Magical,
				Ability.Effect.Duration.UntilNextTurn,
				0.5f)
		},
		Ability.TargetType.SelfOnly);

	/**
	 * ABILITIES FROM THE EXAMPLE GAME
	 */
	public static class Example
	{
		// ACID
		public static Ability acid = new Ability(
			"Acid Spray",
			"Summon a spray of acid to burn all opponents.",
			"PERFORMER:NAME sprays a blast of acid!",
			new Ability.CostToUse[]
			{
				new Ability.CostToUse(Ability.CostToUse.Type.SP, 20)
			},
			new Ability.Effect[]
			{
				new Ability.Effect(
					Ability.Effect.TargetedStat.HP,
					Ability.Effect.SkillType.Magical,
					Ability.Effect.Duration.Immediate,
					1.2f)
			},
			Ability.TargetType.AllOpponents);

		// BITE
		public static Ability bite = new Ability(
			"Bite",
			"Sink your teeth into a single opponent.",
			"PERFORMER:NAME bites TARGET:NAME!",
			null,
			new Ability.Effect[]
			{
				new Ability.Effect(
					Ability.Effect.TargetedStat.HP,
					Ability.Effect.SkillType.Physical,
					Ability.Effect.Duration.Immediate,
					1.0f)
			},
			Ability.TargetType.SingleOpponent);

		// CREEPY CRAWL
		public static Ability creepyCrawl = new Ability(
			"Creepy Crawl",
			"Crawl unsettlingly up an opponent's legs to distract them.",
			"PERFORMER:NAME scuttles up TARGET:NAME's leg!",
			null,
			new Ability.Effect[]
			{
				new Ability.Effect(
					Ability.Effect.TargetedStat.Strength,
					Ability.Effect.SkillType.Physical,
					Ability.Effect.Duration.UntilNextTurn,
					0.9f),
				new Ability.Effect(
					Ability.Effect.TargetedStat.Magic,
					Ability.Effect.SkillType.Physical,
					Ability.Effect.Duration.UntilNextTurn,
					0.9f),
				new Ability.Effect(
					Ability.Effect.TargetedStat.Speed,
					Ability.Effect.SkillType.Physical,
					Ability.Effect.Duration.UntilNextTurn,
					0.9f)
			},
			Ability.TargetType.SingleOpponent);

		// JIGGLE
		public static Ability jiggle = new Ability(
			"Jiggle",
			"Jiggle rapidly, absorbing mana from the environment.",
			"PERFORMER:NAME jiggles threateningly!",
			null,
			new Ability.Effect[]
			{
				new Ability.Effect(
					Ability.Effect.TargetedStat.SP,
					Ability.Effect.SkillType.Physical,
					Ability.Effect.Duration.Immediate,
					0.5f)
			},
			Ability.TargetType.SelfOnly);

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