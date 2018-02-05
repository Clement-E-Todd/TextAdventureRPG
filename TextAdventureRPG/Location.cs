using System;
using System.Collections.Generic;

namespace TextAdventureRPG
{
	public class Location : InteractableElement
	{
		public string name;
		string description;
		public List<InteractableElement> interactableElements = new List<InteractableElement>();
		public RandomEncounterGenerator randomEncounterGenerator;

		public Location(string name, string description, RandomEncounterGenerator randomEncounterGenerator = null)
		{
			this.name = name;
			this.description = description;
			this.randomEncounterGenerator = randomEncounterGenerator;

			interactions.Add(new GoToLocation(this));
		}

		public void ShowDescription()
		{
			Console.Clear();
			Console.WriteLine(name.ToUpper());
			Console.WriteLine("");
			Console.WriteLine(description);
		}

		public List<Interaction> GetPlayerOptions()
		{
			List<Interaction> options = new List<Interaction>();

			foreach (InteractableElement element in interactableElements)
			{
				foreach (Interaction interaction in element.interactions)
				{
					options.Add(interaction);
				}
			}

			return options;
		}
		
		
	}
}
