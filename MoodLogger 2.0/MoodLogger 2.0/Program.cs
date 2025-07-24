using System;
using System.Collections.Generic;

class Character
{
	public string Name { get; set; }
	public int Health { get; set; } = 100;
	public int Money { get; set; } = 500;
	public int Happiness { get; set; } = 100;
}

class Program
{
	static Random rand = new Random();

	static void Main()
	{
		Console.Title = "🎮 LifeSimulator - Live Your Virtual Life!";
		Console.WriteLine("👋 Welcome to LifeSimulator!");
		Console.Write("👤 Enter your name: ");
		string name = Console.ReadLine();

		Character player = new Character { Name = name };
		Console.WriteLine($"\nHello {player.Name}! Let's begin your 30-day life journey...\n");

		for (int day = 1; day <= 30; day++)
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"\n📅 Day {day} of your life...");
			Console.ResetColor();

			SimulateDay(player);

			Console.ForegroundColor = ConsoleColor.Cyan;
			Console.WriteLine($"❤️ Health: {player.Health} | 💰 Money: {player.Money} | 😃 Happiness: {player.Happiness}");
			Console.ResetColor();

			if (player.Health <= 0)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("\n💀 You passed away due to poor health. Game Over!");
				Console.ResetColor();
				return;
			}

			Console.Write("▶️ Press ENTER for next day...");
			Console.ReadLine();
		}

		int finalScore = (player.Health + player.Money + player.Happiness) / 3;
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine($"\n🏁 Your life journey has ended, {player.Name}.");
		Console.WriteLine($"📊 Final Score: {finalScore}/100");

		if (finalScore >= 80) Console.WriteLine("🌟 You lived an amazing life!");
		else if (finalScore >= 50) Console.WriteLine("🙂 Not bad, but could be better!");
		else Console.WriteLine("😢 Tough life, but you made it!");

		Console.ResetColor();
	}

	static void SimulateDay(Character player)
	{
		var events = new List<(string, string[], int[], int[], int[])>
		{
			("You find a lost wallet on the street.",
				new[] { "Take the money", "Return it to police", "Ignore it" },
				new[] { 0, 0, 0 }, new[] { 300, 0, 0 }, new[] { -10, 10, 0 }),

			("You got sick today.",
				new[] { "Go to doctor", "Rest at home", "Ignore it" },
				new[] { -10, -5, -20 }, new[] { -100, 0, 0 }, new[] { 0, -5, -10 }),

			("You won a small lottery!",
				new[] { "Celebrate", "Save it", "Donate it" },
				new[] { 0, 0, 0 }, new[] { 100, 100, 0 }, new[] { 10, 0, 10 }),

			("Your boss offers a risky promotion.",
				new[] { "Accept", "Decline", "Quit job" },
				new[] { -5, 0, 0 }, new[] { 500, 0, -200 }, new[] { -10, 0, 20 }),

			("A cute cat follows you home.",
				new[] { "Adopt", "Find its owner", "Shoo it away" },
				new[] { 0, 0, 0 }, new[] { -50, 0, 0 }, new[] { 10, 5, -5 }),

			("You binge-watch a whole series all night.",
				new[] { "It was worth it", "Should've slept", "Go outside instead" },
				new[] { -10, -5, 5 }, new[] { 0, 0, 0 }, new[] { 15, 5, 10 })
		};

		var ev = events[rand.Next(events.Count)];
		Console.WriteLine($"🎲 Event: {ev.Item1}");

		for (int i = 0; i < ev.Item2.Length; i++)
		{
			Console.WriteLine($"{i + 1}. {ev.Item2[i]}");
		}

		int choice = 0;
		while (choice < 1 || choice > ev.Item2.Length)
		{
			Console.Write("🧠 Your choice (1-3): ");
			int.TryParse(Console.ReadLine(), out choice);
		}

		player.Health += ev.Item3[choice - 1];
		player.Money += ev.Item4[choice - 1];
		player.Happiness += ev.Item5[choice - 1];

		// 🔧 .NET 5+ uyumlu Clamp yerine:
		player.Health = Math.Max(0, Math.Min(100, player.Health));
		player.Happiness = Math.Max(0, Math.Min(100, player.Happiness));
		player.Money = Math.Max(0, player.Money);
	}
}
