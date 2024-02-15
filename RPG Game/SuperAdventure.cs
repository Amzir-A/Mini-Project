public class SuperAdventure
{
	public Monster CurrentMonster { get; set; }
	public Player CurrentPlayer { get; set; }
	public bool finished = false;

	public bool PlayerTurn = true;
	public bool PlayerIsWinner = false;

	public Random rand = new();

	public SuperAdventure(Monster currentMonster, Player currentPlayer)
	{
		CurrentMonster = currentMonster;
		CurrentPlayer = currentPlayer;
	}

	public void Fight()
	{
		if (PlayerTurn)
		{
			Console.WriteLine("\nWhat would you like to do?");
			Console.WriteLine("1. Attack");
			Console.WriteLine("2. Change Weapon");
			Console.WriteLine("3. Drink Potion");
			Console.WriteLine("4. Run");
			int choice;
			bool valid;
			do
			{
				valid = int.TryParse(Console.ReadLine(), out choice);
			} while (valid == false || choice < 1 || choice > 3);

			switch (choice)
			{
				case 1:
					CurrentPlayer.Attack(CurrentMonster);
					break;
				case 2:
					Console.WriteLine("\nWhich weapon would you like to use?\n");
					int i = 1;
					foreach (Weapon weapon in CurrentPlayer.Weapons)
					{
						Console.WriteLine($"{i}. {weapon.Name}");
						i++;
					}
					int weaponChoice;
					bool weaponValid;
					do
					{
						weaponValid = int.TryParse(Console.ReadLine(), out weaponChoice);
					} while (weaponValid == false && weaponChoice < 1 && weaponChoice > i);
					CurrentPlayer.CurrentWeapon = CurrentPlayer.Weapons[weaponChoice - 1];
					Console.WriteLine("\n\n");
					break;
				case 3:
					foreach (StackedItem stackedItem in CurrentPlayer.Inventory)
					{
						if (stackedItem.Details?.ID == World.ITEM_ID_HEALING_POTION)
						{
							CurrentPlayer.CurrentHitPoints += 10;
							CurrentPlayer.RemoveItem(stackedItem.Details, 1);

							Console.WriteLine("You drank a potion and gained 10 hit points\n");
							CurrentPlayer.CurrentHitPoints += 10;
							break;
						}
					}
					Console.WriteLine("You don't have any potions\n");
					break;
				case 4:
					Console.WriteLine("You ran away!\n");
					CurrentPlayer.CurrentLocation = World.LocationByID(World.LOCATION_ID_HOME);
					finished = true;
					break;
			}
		}
		else
		{
			CurrentMonster.Attack(CurrentPlayer);
		}
		PlayerTurn = !PlayerTurn;
		if (CurrentPlayer.CurrentHitPoints <= 0)
		{
			Console.WriteLine("You have been defeated!\nReturning home\n");
			CurrentPlayer.CurrentLocation = World.LocationByID(World.LOCATION_ID_HOME);
			CurrentPlayer.CurrentHitPoints = CurrentPlayer.MaximumHitPoints;

			finished = true;
			if (CurrentPlayer.Inventory.Count > 1)
			{
				CurrentPlayer.RemoveItem(World.ItemByID(World.ITEM_ID_ADVENTURER_PASS), 1);

				Item? LostItem = CurrentPlayer.Inventory[rand.Next(0, CurrentPlayer.Inventory.Count)].Details;
				CurrentPlayer.RemoveItem(LostItem, 1);
				Console.WriteLine($"You lost a {LostItem?.Name}\n");

				CurrentPlayer.AddItem(World.ItemByID(World.ITEM_ID_ADVENTURER_PASS), 1);
			}
		}
		else if (CurrentMonster.CurrentHitPoints <= 0)
		{
			Console.WriteLine($"You have defeated the {CurrentMonster.Name}!\n");
			finished = true;

			CurrentMonster.CurrentHitPoints = CurrentMonster.MaximumHitPoints;
			PlayerIsWinner = true;

			CurrentPlayer.Gold += rand.Next(CurrentMonster.MaximumHitPoints / 2, CurrentMonster.MaximumHitPoints);
			Console.WriteLine($"You found {CurrentMonster.MaximumHitPoints / 2} gold\n");

			if (rand.Next(1, 100) < 70)
			{
				Item FoundItem = CurrentMonster.DropItem[rand.Next(0, CurrentMonster.DropItem.Count)];
				CurrentPlayer.AddItem(FoundItem, 1);
				Console.WriteLine($"You found a {FoundItem.Name}\n");
			}

			int xp = rand.Next(500, 1000);
			CurrentPlayer.IncreaseExp(xp);
			Console.WriteLine($"You gained {xp} experience\n");
		}
	}
}