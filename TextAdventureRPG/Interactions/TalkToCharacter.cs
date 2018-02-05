using System;

namespace TextAdventureRPG
{
	class TalkToCharacter : Interaction
	{
		Character character;

		public TalkToCharacter(Character character)
			: base("Talk to " + character.name)
		{
			this.character = character;
		}

		public override void DoAction()
		{
			Console.Clear();
			if (character.talkText != null && character.talkText.Length > 0)
			{
				Console.WriteLine(character.name.ToUpper() + ": " + character.talkText);
			}
			else
			{
				Console.WriteLine(character.name.ToUpper() + ": ...");
			}
			Console.WriteLine("");
			Console.WriteLine("[Press ENTER to continue]");
			Console.ReadLine();
		}
	}
}
