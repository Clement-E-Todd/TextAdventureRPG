using System;

class Pronouns
{
	public string they;
	public string them;
	public string their;
	public string theirs;
	public string themself;

	public Pronouns(string they, string them, string their, string theirs, string themself)
	{
		this.they = they;
		this.them = them;
		this.their = their;
		this.theirs = theirs;
		this.themself = themself;
	}

	public static class Presets
	{
		public static Pronouns feminine = new Pronouns("she", "her", "her", "hers", "herself");
		public static Pronouns masculine = new Pronouns("he", "him", "his", "his", "himself");
		public static Pronouns they = new Pronouns("they", "them", "their", "theirs", "themself");
		public static Pronouns ey = new Pronouns("ey", "em", "eir", "eirs", "eirself");
		public static Pronouns e = new Pronouns("e", "em", "eir", "eirs", "eirself");
		public static Pronouns per = new Pronouns("per", "per", "pers", "pers", "perself");
		public static Pronouns sie = new Pronouns("sie", "sir", "hir", "hirs", "hirself");
		public static Pronouns ve = new Pronouns("ve", "ver", "vis", "vers", "verself");
		public static Pronouns zie = new Pronouns("zie", "zim", "zir", "zirs", "zirself");
		public static Pronouns it = new Pronouns("it", "it", "its", "its", "itself");

		public static Pronouns[] nonbinary = new Pronouns[] { they, ey, e, per, sie, ve, zie, it };
	}

	public static Pronouns CreateCustom()
	{
		Console.WriteLine("Enter a subject pronoun (ie. 'they')");
		Console.WriteLine("Example in a sentence: 'They dodged the fireball!'");
		string they = Console.ReadLine();

		Console.WriteLine("Enter an object pronoun (ie. 'them')");
		Console.WriteLine("Example in a sentence: 'The fireball narrowly misses them!'");
		string them = Console.ReadLine();

		Console.WriteLine("Enter a possessive ponoun (ie. 'their')");
		Console.WriteLine("Example in a sentence: 'Their quick reflexes really paid off!'");
		string their = Console.ReadLine();

		Console.WriteLine("Enter another possessive ponoun (ie. 'theirs')");
		Console.WriteLine("Example in a sentence: 'Victory is theirs!'");
		string theirs = Console.ReadLine();

		Console.WriteLine("Enter a reflexive ponoun (ie. 'themself')");
		Console.WriteLine("Example in a sentence: 'They are very proud of themself!'");
		string themself = Console.ReadLine();

		return new Pronouns(they, them, their, theirs, themself);
	}
}