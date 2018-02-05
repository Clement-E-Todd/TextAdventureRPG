using System;

namespace TextAdventureRPG
{
	public class Character : InteractableElement
	{
		public string name;
		public string talkText;

		public Character(string name, string talkText = null)
		{
			this.name = name;
			this.talkText = talkText;
			
			interactions.Add(new TalkToCharacter(this));
			interactions.Add(new AttackCharacter(this));
		}

		public Character CreateCopy()
		{
			Character characterCopy = new Character(name, talkText);

			return characterCopy;
		}
	}
}