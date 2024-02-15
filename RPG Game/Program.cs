class Program
{
    static void Main(string[] args)
    {
        bool Quit = false;

        Player player = new Player("Player", World.LocationByID(World.LOCATION_ID_HOME), World.WeaponByID(World.WEAPON_ID_RUSTY_SWORD), 10, 10);

        Console.WriteLine("The game!\n");
        Console.WriteLine("The people in your town are being terrorized by giant spiders.");
        Console.WriteLine("You decide to do what you can to help.\n");
        Console.WriteLine("Objective: complete all quests\n");

        Console.WriteLine("How should I call you?.\n");
        player.Name = Console.ReadLine() ?? "Player";

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
            } while (valid == false && choice < 1 && choice > 4);

            switch (choice)
            {
                case 1:
                    Console.WriteLine($"Player: {player.Name}");
                    Console.WriteLine($"Hit Points: {player.CurrentHitPoints}");
                    Console.WriteLine($"Weapon: {player.CurrentWeapon?.Name}");
                    Console.WriteLine($"Location: {player.CurrentLocation?.Name}");
                    Console.WriteLine($"Inventory: {player.Inventory.Count}");
                    foreach (StackedItem stackedItem in player.Inventory)
                    {
                        Console.WriteLine($"    {stackedItem.Quantity} {stackedItem.Details?.Name}");
                    }
                    Console.WriteLine($"Quests: {player.Quests.Count}");
                    foreach (Quest quest in player.Quests)
                    {
                        Console.WriteLine($"    {quest.Name}" + (quest.IsCompleted ? " - Completed" : ""));
                        Console.WriteLine($"       - {quest.Description}");
                    }
                    break;
                case 2:
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
                    } while (Move_valid == false || Move_options_valid == false);

                    if (Move_options_valid)
                    {
                        Location? toGoLocation = player.CurrentLocation?.GetLocationAt(Move_choice.ToString());

                        if (toGoLocation?.ID == World.LOCATION_ID_GUARD_POST &&
                            (World.QuestByID(World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN)?.IsCompleted == false ||
                            World.QuestByID(World.QUEST_ID_CLEAR_FARMERS_FIELD)?.IsCompleted == false))
                        {
                            Console.WriteLine("Turn back at once, peasant! Unless thee hast proof of thy grit");
                        }

                        else
                        {
                            player.CurrentLocation = toGoLocation;
                            Console.WriteLine($"\nYou are now at: {player.CurrentLocation?.Name}.\n - {player.CurrentLocation?.Description}");


                            Quest? FarmQuest = World.QuestByID(World.QUEST_ID_CLEAR_FARMERS_FIELD);
                            if (toGoLocation?.ID == World.LOCATION_ID_FARMHOUSE)
                            {
                                if (FarmQuest?.IsCompleted == true && FarmQuest?.RewardCollected == false)
                                {
                                    Console.WriteLine("\n\nThank you for killing the snakes. Here, take this potion as a reward.");
                                    Console.WriteLine("You obtained a healing potion.");
                                    player.AddItem(World.ItemByID(World.ITEM_ID_HEALING_POTION), 1);

                                    FarmQuest.RewardCollected = true;
                                }
                                else
                                {
                                    player.AcceptQuest(FarmQuest);
                                }
                            }


                            Quest? AlchemistQuest = World.QuestByID(World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN);
                            if (toGoLocation?.ID == World.LOCATION_ID_ALCHEMIST_HUT)
                            {
                                if (AlchemistQuest?.IsCompleted == true && AlchemistQuest?.RewardCollected == false)
                                {
                                    Console.WriteLine("\n\nThank you for killing the rats. Here, take this potion and new weapon as a reward.");
                                    Console.WriteLine("You obtained a healing potion.");
                                    Console.WriteLine("You obtained a healing potion.");

                                    player.Weapons.Add(World.WeaponByID(World.WEAPON_ID_CLUB) ?? new Weapon(World.WEAPON_ID_CLUB, "Club", 10));
                                    player.AddItem(World.ItemByID(World.ITEM_ID_HEALING_POTION), 1);

                                    AlchemistQuest.RewardCollected = true;
                                }
                                else
                                {
                                    player.AcceptQuest(AlchemistQuest);
                                }
                            }

                            Quest? ForestQuest = World.QuestByID(World.QUEST_ID_COLLECT_SPIDER_SILK);
                            if (toGoLocation?.ID == World.LOCATION_ID_BRIDGE)
                            {
                                if (ForestQuest?.IsCompleted == true && ForestQuest?.RewardCollected == false)
                                {
                                    Console.WriteLine("\n\nThank you for killing the Spiders, the last monsters in the village.\n You have won the game!.");
                                    ForestQuest.RewardCollected = true;

                                    Console.WriteLine("Do you want to quit? (Y/N)");
                                    char quit_choice;
                                    bool quit_valid;
                                    do
                                    {
                                        quit_valid = char.TryParse(Console.ReadLine(), out quit_choice);
                                    } while (quit_valid == false || char.ToLower(quit_choice) != 'y' && char.ToLower(quit_choice) != 'n');

                                    if (char.ToLower(quit_choice) == 'y')
                                    {
                                        Quit = true;
                                    }
                                }
                                else
                                {
                                    player.AcceptQuest(ForestQuest);
                                }
                            }

                        }
                    }

                    break;
                case 3:
                    Monster? monster = player.CurrentLocation?.MonsterLivingHere;
                    if (monster != null)
                    {
                        if (player.CurrentLocation?.AmountOfMonsters > 0)
                        {
                            SuperAdventure superAdventure = new SuperAdventure(monster, player);
                            Console.WriteLine("\nYou are fighting a \n" + monster.Name);

                            while (superAdventure.finished == false)
                            {
                                superAdventure.Fight();
                            }
                            if (superAdventure.PlayerIsWinner)
                            {
                                player.CurrentLocation.AmountOfMonsters -= 1;
                                if (player.CurrentLocation.AmountOfMonsters == 0)
                                {
                                    player.CurrentLocation.MonsterLivingHere = null;
                                    Console.WriteLine("Quest Completed, return to receive your reward.");
                                    if (player.CurrentLocation.ID == World.LOCATION_ID_FARM_FIELD)
                                    {
                                        World.QuestByID(World.QUEST_ID_CLEAR_FARMERS_FIELD)?.SetCompleted();
                                    }
                                    else if (player.CurrentLocation.ID == World.LOCATION_ID_ALCHEMISTS_GARDEN)
                                    {
                                        World.QuestByID(World.QUEST_ID_CLEAR_ALCHEMIST_GARDEN)?.SetCompleted();
                                    }
                                    else if (player.CurrentLocation.ID == World.LOCATION_ID_SPIDER_FIELD)
                                    {
                                        World.QuestByID(World.QUEST_ID_COLLECT_SPIDER_SILK)?.SetCompleted();
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("There are no monsters here.");
                    }
                    break;
                case 4:
                    Quit = true;
                    break;
                default:

                    break;
            }
            if (Quit) { break; }
        }
    }
}