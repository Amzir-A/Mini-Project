public class Monster
{
	public int ID { get; set; }
	public string Name { get; set; }
	public int CurrentHitPoints { get; set; }
	public int MaximumDamage { get; set; }
	public int MaximumHitPoints { get; set; }

	public Monster(int id, string name, int maximumDamage, int maximumHitPoints, int currentHitPoints)
	{
		ID = id;
		Name = name;
		MaximumHitPoints = maximumHitPoints;
		MaximumDamage = maximumDamage;
		CurrentHitPoints = currentHitPoints;
	}
	public void TakeDamage(int damage)
	{
		CurrentHitPoints -= damage;
		if (CurrentHitPoints <= 0)
		{
			Console.WriteLine($"{Name} has been defeated!");
		}
		else
		{
			Console.WriteLine($"{Name} took {damage} damage and has {CurrentHitPoints} health remaining!");
		}
	}

	public void Attack(Player target)
	{
		Random random = new Random();
		int damageDealt = random.Next(1, MaximumDamage + 1); // Random damage within the monster's maximum damage
		Console.WriteLine($"{Name} attacks {target.Name} for {damageDealt} damage!");
		target.CurrentHitPoints -= damageDealt;
		if (target.CurrentHitPoints <= 0)
		{
			Console.WriteLine($"{target.Name} has been defeated!");
		}
		else
		{
			Console.WriteLine($"{target.Name} has {target.CurrentHitPoints} health remaining!");
		}
	}
}