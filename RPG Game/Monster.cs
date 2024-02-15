using System.Security.Cryptography;

public class Monster
{
	public int ID { get; set; }
	public string Name { get; set; }
	public int CurrentHitPoints { get; set; }
	public int MaximumDamage { get; set; }
	public int MaximumHitPoints { get; set; }

	public List<Item> DropItem { get; set; }

	public Random rand = new();

	public Monster(int id, string name, int maximumDamage, int maximumHitPoints, int currentHitPoints, List<Item> dropItem)
	{
		ID = id;
		Name = name;
		MaximumHitPoints = maximumHitPoints;
		MaximumDamage = maximumDamage;
		CurrentHitPoints = currentHitPoints;
		DropItem = dropItem;
	}

	public void Attack(Player player)
	{
		int damage = rand.Next(0, MaximumDamage);
		player.CurrentHitPoints -= damage;
		Console.WriteLine($"The {Name} did {damage} points of damage to you");
		Console.WriteLine($"You have {player.CurrentHitPoints} hit points remaining");
	}
}