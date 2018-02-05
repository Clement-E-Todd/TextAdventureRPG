using System;

namespace TextAdventureRPG
{
	class AttackCharacter : Interaction
	{
		Character character;

		public AttackCharacter(Character character)
			: base("Attack " + character.name)
		{
			this.character = character;
		}

		public override void DoAction()
		{
			EnemyEncounter encounter = new EnemyEncounter(new Character[] { character });
			encounter.Start();
		}
	}
}
