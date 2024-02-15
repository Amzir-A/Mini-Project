﻿class Program
{
    static void Main(string[] args)
    {
        bool Quit = false;

        Player player = new Player("Player", World.LocationByID(World.LOCATION_ID_HOME), World.WeaponByID(World.WEAPON_ID_RUSTY_SWORD), 10, 10);
        Monster rat = new Monster(World.MONSTER_ID_RAT, "rat", 1, 3, 3);
        Location location = new Location(World.LOCATION_ID_ALCHEMIST_HUT, "Alchemist's hut", "There are many strange plants on the shelves.", null, null);
        Console.WriteLine("Welcome to the game!\n");
        Console.WriteLine("You are in a world full of monsters. It is up to you to save the world.\n");

        while (true)
        {
            Console.WriteLine("\nWhat would you like to do (Enter a number)?");
            Console.WriteLine("1. See game stats");
            Console.WriteLine("2. Move");
            Console.WriteLine("3. Fight");
            Console.WriteLine("4. Quit");

            int choice;
            bool valid;
            do
            {
                valid = int.TryParse(Console.ReadLine(), out choice);
            } while (!valid || choice < 1 || choice > 4);

            switch (choice)
            {
                case 1:
                    // See game stats
                    break;
                case 2:
                    // Move
                    Move(player);
                    break;
                case 3:
                    Fight(player, rat, location);
                    break;
                case 4:
                    Quit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                    break;
            }

            if (Quit)
            {
                break;
            }
        }
    }

    static void Move(Player player)
    {
        Console.WriteLine("Where would you like to go?");
        Console.WriteLine($"You are at: {player.CurrentLocation?.Name}. {player.CurrentLocation?.Compass()}");

        char Move_choice;
        bool Move_valid;
        string? Move_next_options;
        bool Move_options_valid = false;

        do
        {
            Move_valid = char.TryParse(Console.ReadLine(), out Move_choice);

            if (Move_valid)
            {
                if (char.ToUpper(Move_choice) == 'C')
                {
                    Console.WriteLine("You didn't move.");
                    break;
                }

                Move_next_options = player.CurrentLocation?.NextLocationOptions();

                for (int i = 0; i < Move_next_options?.Length; i++)
                {
                    if (char.ToLower(Move_choice) == char.ToLower(Move_next_options[i]))
                    {
                        Move_options_valid = true;
                        break;
                    }
                }
            }
        } while (!Move_valid || !Move_options_valid);

        if (Move_options_valid)
        {
            player.CurrentLocation = player.CurrentLocation?.GetLocationAt(Move_choice.ToString());
            Console.WriteLine($"\nYou are now at: {player.CurrentLocation?.Name}.\n - {player.CurrentLocation?.Description}");
        }
    }

    static void Fight(Player player, Monster monster, Location location)
    {
        if (location.MonsterLivingHere != null)
            Console.WriteLine($"A wild {monster.Name} has appeared!");
    }
}