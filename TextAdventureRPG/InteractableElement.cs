using System;
using System.Collections.Generic;

namespace TextAdventureRPG
{
	public abstract class InteractableElement
	{
		public List<Interaction> interactions = new List<Interaction>();
	}
}