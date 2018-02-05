using System;
using System.Collections.Generic;

namespace TextAdventureRPG
{
	public static class Game
	{
		public static  bool quitRequested = false;

		static Location currentLocation;

		public static void Start(Location firstLocation)
		{
			currentLocation = firstLocation;

			while (quitRequested == false)
			{
				currentLocation.ShowDescription();
				List<Interaction> options = currentLocation.GetPlayerOptions();
				HandlePlayerInput(options);
			}
		}

		public static void SetCurrentLocation(Location location)
		{
			currentLocation = location;
		}

		static void HandlePlayerInput(List<Interaction> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				int displayNumber = i + 1;
				Console.WriteLine("[" + displayNumber + "] " + options[i].description);
			}
			Console.WriteLine("[Q] Quit the game");

			bool validInputReceived = false;
			int optionIndex = -1;

			do
			{
				string userInputString = Console.ReadLine().ToUpper();

				if (userInputString == "Q")
				{
					Console.WriteLine("Are you sure you want to quit? (yes / no)");
					userInputString = Console.ReadLine().ToUpper();

					if (userInputString == "YES" || userInputString == "Y")
					{
						Game.quitRequested = true;
						break;
					}
					else
					{
						continue;
					}
				}

				int userInputNumber;
				bool inputIsInteger = int.TryParse(userInputString, out userInputNumber);

				validInputReceived = (inputIsInteger && userInputNumber >= 1 && userInputNumber <= options.Count);

				if (validInputReceived == false)
				{
					Console.WriteLine("Error: please type a number between 1 and " + options.Count.ToString() + ".");
				}
				else
				{
					optionIndex = userInputNumber - 1;
				}

			} while (validInputReceived == false);

			if (validInputReceived)
			{
				options[optionIndex].DoAction();
			}
		}
	}
}