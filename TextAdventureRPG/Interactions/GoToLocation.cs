namespace TextAdventureRPG
{
	class GoToLocation : Interaction
	{
		Location location;

		public GoToLocation(Location location)
			: base("Go to " + location.name)
		{
			this.location = location;
		}

		public override void DoAction()
		{
			Game.SetCurrentLocation(location);

			if (location.randomEncounterGenerator != null)
			{
				EnemyEncounter encounter = location.randomEncounterGenerator.CreateEncounter();

				if (encounter != null)
				{
					encounter.Start();
				}
			}
		}
	}
}
