using System.Security.Cryptography.X509Certificates;

public class Player
{
	public string Name { get; set; }
	public Location? CurrentLocation { get; set; }
	public Weapon? CurrentWeapon { get; set; }
	public int MaximumHitPoints { get; set; }
	public int CurrentHitPoints { get; set; }

	public List<Quest> Quests = [];
	public int Gold = 0;

	public Random rand = new();

	public List<StackedItem> Inventory;
	public List<Weapon> Weapons = [];


	public Player(string name, Location? currentLocation, Weapon? currentWeapon, int maximumHitPoints, int currentHitPoints)
	{
		Name = name;
		CurrentLocation = currentLocation;
		CurrentWeapon = currentWeapon;
		CurrentHitPoints = currentHitPoints;
		MaximumHitPoints = maximumHitPoints;
		Inventory = [];

		Weapons.Add(currentWeapon ?? new Weapon(World.WEAPON_ID_RUSTY_SWORD, "Rusty sword", 5));
	}

	public void AddItem(Item? item, int quantity)
	{
		foreach (StackedItem stackedItem in Inventory)
		{
			if (stackedItem.Details?.ID == item?.ID)
			{
				stackedItem.Quantity += quantity;
				return;
			}
		}
		Inventory.Add(new StackedItem(item, quantity));
	}

	public void RemoveItem(Item? item, int quantity)
	{
		foreach (StackedItem stackedItem in Inventory)
		{
			if (stackedItem.Details?.ID == item?.ID)
			{
				stackedItem.Quantity -= quantity;
				if (stackedItem.Quantity <= 0)
				{
					Inventory.Remove(stackedItem);
				}
				return;
			}
		}
	}

	public void AcceptQuest(Quest? quest)
	{
		foreach (Quest player_quest in Quests)
		{
			if (player_quest.ID == quest?.ID)
			{
				Console.WriteLine($"\nYou have a quest to {quest.Description}");
				return;
			}
		}

		Console.WriteLine("\nYou have a new quest:\n");
		Console.WriteLine(quest?.Description);

		Quests.Add(quest ?? new Quest(0, "No Quest", "No Description"));
	}


	public void Attack(Monster monster)
	{
		int damage = rand.Next(0, CurrentWeapon?.MaximumDamage ?? 0);
		monster.CurrentHitPoints -= damage;
		Console.WriteLine($"You did {damage} points of damage to {monster.Name}");
		Console.WriteLine($"{monster.Name} has {monster.CurrentHitPoints} hit points remaining");
	}


	public void DrinkPotion()
	{
		// Method implementation
	}
}