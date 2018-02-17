using System;

static class Program
{
	public static Random random = new Random();

	static void Main()
	{
		// Run the example game. Remove this line to stop the example game from running.
		ExampleGame.Run();
	}

	public static void PressEnterToContinue()
	{
		Console.WriteLine("");
		Console.WriteLine("[Press ENTER to continue]");
		Console.ReadLine();
	}
}
