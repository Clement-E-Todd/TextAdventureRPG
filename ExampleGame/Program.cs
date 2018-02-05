using TextAdventureRPG;

namespace ExampleGame
{
	class Program
	{
		static void Main(string[] args)
		{
			Location testHub = new Location(
				"the valley",
				"A strange house stands before you, and a dark and evil forest lies beyond."
			);

			Location testHouse = new Location(
				"the strange house",
				"The house is much nicer-looking on the inside. An old man sits by the fire."
			);

			Location dangerousArea = new Location(
				"the dark woods",
				"The area seems clear for now... you'd best head back before something nasty finds you!",
				new RandomEncounterGenerator(
					new Character[] {
						new Character("Spider"),
						new Character("Goblin"),
						new Character("Snake")
					},
					0.5f, 2, 4
				)
			);

			testHub.interactableElements.Add(testHouse);
			testHub.interactableElements.Add(dangerousArea);

			testHouse.interactableElements.Add(testHub);
			testHouse.interactableElements.Add(new Character("old man", "Why hello! Make yourself at home."));

			dangerousArea.interactableElements.Add(testHub);

			Game.Start(testHub);
		}
	}
}
