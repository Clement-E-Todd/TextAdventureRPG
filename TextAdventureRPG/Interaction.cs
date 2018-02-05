namespace TextAdventureRPG
{
	public abstract class Interaction
	{
		public string description;

		public Interaction(string description)
		{
			this.description = description;
		}

		public abstract void DoAction();
	}
}
