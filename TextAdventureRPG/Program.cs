using System;

/**
 * PROGRAM
 * The program which runs the game.
 */
static class Program
{
	// A random number generator that can be used from anywhere in the game
	public static Random random = new Random();

	// Entry point for the program. Calls the function which runs the game.
	static void Main()
	{
		// Run the example game. Remove this line to stop the example game from running.
		ExampleGame.Run();
	}

	// Convenience function for prompting the player to press ENTER to continue...
	// because that's something that happens an awful lot!
	public static void PressEnterToContinue()
	{
		Console.WriteLine("");
		Console.WriteLine("[Press ENTER to continue]");
		Console.ReadLine();
	}
}
