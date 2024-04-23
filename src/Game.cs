using System;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;

	public int keysCollected;
	private bool gameWon = false; 

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player();
		CreateRooms();
	}

	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room riverbank = new Room("at a riverbank, your raft still is lingering.");
		Room beginForest = new Room(" at the beginning of the forest");
		Room deepForest = new Room(" At the middle of the forest, there is a giant tree");
		Room ancientTree = new Room(" In the giant tree, what is this?");
		Room treeSide = new Room("a sideway room, dimly lit");
		Room cavernsEntrance = new Room(" At the beginning of the Shadowed Sanctum");
		Room sanctumRoom1 = new Room(" In the hallway of the Shadowed Sanctum");
		Room sanctumRoom2 = new Room(" You're in the first room, but you see a big enemy carrying a KEY?");
		Room sanctumRoom3 = new Room(" You're in a hallway again, where should you go next?");
		Room sanctumRoom4 = new Room(" You're in the second room of the sanctum, and again, a BIG monster");
		Room sanctumExit = new Room(" You're at the exit of the sanctum, phew");
		Room ascendedHeaven = new Room(" You're at the gate of the Lightborne, to ascend and get 4 keys, you must defeat the Light One");



		riverbank.AddExit("forward", beginForest);
		beginForest.AddExit("backward", riverbank);
		beginForest.AddExit("forward", deepForest);
		deepForest.AddExit("backwards", beginForest);
		deepForest.AddExit("forward", ancientTree);
		ancientTree.AddExit("backwards", deepForest);
		ancientTree.AddExit("left", treeSide);
		treeSide.AddExit("back", ancientTree);
		ancientTree.AddExit("forward", cavernsEntrance);
		cavernsEntrance.AddExit("forward", sanctumRoom1);
		cavernsEntrance.AddExit("backwards", ancientTree);
		sanctumRoom1.AddExit("forward", sanctumRoom2);
		sanctumRoom1.AddExit("backwards", cavernsEntrance);
		sanctumRoom2.AddExit("forward", sanctumRoom3);
		sanctumRoom2.AddExit("backwards", sanctumRoom1);
		sanctumRoom3.AddExit("forward", sanctumRoom4);
		sanctumRoom3.AddExit("backwards", sanctumRoom2);
		sanctumRoom4.AddExit("forward", sanctumExit);
		sanctumRoom4.AddExit("backwards", sanctumRoom3);
		sanctumExit.AddExit("forward", ascendedHeaven);
		ascendedHeaven.AddExit("backwards", sanctumRoom4);
		




		
		player.CurrentRoom = riverbank;

		// Create your Items here
		// weight, dmg, descr
		Item darkkey = new Item(1,0,"Dark Key | The First of the 4 Keys");
		Item lightkey = new Item(1,0,"Light Key | The Second of the 4 Keys");
		Item soulkey = new Item(1,0,"Soul Key | The Third of the 4 Keys");
		Item spiritkey = new Item(1,0,"Spirit Key | The Last of the 4 Keys");
		Item sword = new Item(10,25000, "| Sword | Condition: New | Damage: 25 | ");
		Item moonlightdagger = new Item(10,50, "| Moonlight Dagger | Condition: Ungodly | Damage: 50 | ");
		Item sunfirebow = new Item(10,75, "| Sunfire Bow | Condition: On fire! | Damage: 75 | ");
		Item thunderclapstaff = new Item(10,125, "| Thunderclap Staff | Condition: Thy Be Lightnin! | Damage: 125 | ");
		Item eclipseblades = new Item(10,250, "| Eclipse Blades | Condition: GODLIKE, PEAK. | Damage: 250 | ");
		
		Item wand = new Item (5,5, $"| Wand | Condition: Battle Scarred |Damage: 25  | Description: A wand");
		Item stick = new Item(1,5, "A stick");

		// put items into rooms
		riverbank.Chest.Put("sword", sword); 
		riverbank.Chest.Put("stick", stick);
		treeSide.Chest.Put("moonlightdagger", moonlightdagger);
		treeSide.Chest.Put("darkkey", darkkey);
		// ADD ENEMIES
								//  health armor title dmg, heal upon kill, player
		    Enemy voidslime = new Enemy(25,10, "VoidSlime",2,25, player);
    		riverbank.AddEnemy("VoidSlime", voidslime);
			voidslime.AddLoot(new Item(1, 0, "voidessence"));
			voidslime.AddLoot(new Item(1, 0, "dust"));
			voidslime.AddLoot(new Item(1,0, "A Note: Collect the 4 Void Keys to ascend!"));

            Enemy Borus = new Enemy(100,50, "The Ancient Tree",25,25, player);
            ancientTree.AddEnemy("Borus", Borus);

            Enemy Skeleton = new Enemy(50,50, "The Skelleybones",10,25, player);
            sanctumRoom1.AddEnemy("Skeleton", Skeleton);

            Enemy Snail = new Enemy(10,20, "The Snailiest",1,5, player);
            Enemy BigSnail = new Enemy(10,50, "BIG BOY",1,5, player);
            beginForest.AddEnemy("Snail", Snail);
            deepForest.AddEnemy("BigSnail", BigSnail);

			Enemy Ignis = new Enemy(100,50, "The Flame Warden",25,100, player);
    		sanctumRoom2.AddEnemy("Ignis", Ignis);

			Enemy Thoros = new Enemy(100,50, "The Stormcaller",50,100, player);
			sanctumRoom4.AddEnemy("Thoros", Thoros);

			Enemy Nyx = new Enemy(100,75, "The Shadow Seraph",50,100, player);
			sanctumExit.AddEnemy("Nyx", Nyx);

			Enemy Luminos = new Enemy(200,100, "The Radiant Sentinel",100,150, player);
			ascendedHeaven.AddEnemy("Luminos", Luminos);


			



	}

	//  Main play routine. Loops until end of play.
public void Play()
    {
        PrintWelcome();

        bool finished = false;
        while (!finished)
        {
            Command command = parser.GetCommand();

            if (gameWon)
            {
                Console.WriteLine("You have won the game! You have now ascended to be a Lightborne!");
                return; // Exit the Play method
            }

            finished = ProcessCommand(command);
        }

        Console.WriteLine("Thank you for playing little voidling");
        Console.WriteLine("Press [Enter] to continue.");
        Console.ReadLine();
    }
	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("----START OF DIALOGUE----");
		Console.WriteLine("Welcome... voidborne");
		Console.WriteLine("Sadly, you died! but there is no worry.");
		Console.WriteLine("As a Voidborne you get a second chance in life, to get resurrected as a Lightborne");
		Console.WriteLine("Go now, your time is limited... find a way to the top");
		Console.WriteLine("You are on a small boat, and are approaching a riverbank");
        Console.WriteLine("Killing enemies refuels your Void Energy, and makes you stronger");
        Console.WriteLine("Now go, create, destroy and become one with thy light");
		Console.WriteLine("----END OF DIALOGUE----");
		Console.WriteLine("Confused? type help");
		Console.WriteLine();
		Console.WriteLine();
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}


	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
{
    bool wantToQuit = false;

    if (player.health >= 1)
    {
        player.isAlive = true;
    }

    if (player.health <= 0)
    {
        Console.WriteLine("Your Void fades away (dead)");
        Console.WriteLine("TIP: Slaying Enemies makes you healthier.");
        wantToQuit = true;
        return wantToQuit;
    }

    if (command.IsUnknown())
    {
        Console.WriteLine("I don't know what you mean...");
        return wantToQuit; // false
    }

    switch (command.CommandWord)
    {
        case "help":
            PrintHelp();
            break;
        case "go":
            GoRoom(command);
            player.ShowPlayerHealth();
            break;
        case "look":
            Look();
            break;
        case "quit":
            wantToQuit = true;
            break;
        case "status":
            player.ShowPlayerHealth();
            player.CheckBackpackWeight();
            CheckWeight();
            break;
        case "devKill":
            PrintDKill();
            player.devdamagePlayer();
            break;
        case "take":
            Take(command);
            break;
        case "drop":
            Drop(command);
            break;
        case "use":
            Use(command);
            break;
        case "win":
            WinGame();
            break;
    }


        return wantToQuit;
    }


	// ######################################
	// implementations of user commands:
	// ######################################

	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine();
		Console.WriteLine("------");
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You currently are " + player.CurrentRoom.GetShortDescription());
		Console.WriteLine("You're a Voidborne, search my child, for you must come to the light and be reborn");
		Console.WriteLine("------");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	private void PrintDKill()
	{
		Console.WriteLine("Devkill has been executed Fez");
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	private void GoRoom(Command command)
	{
		if (!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			return;
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to " + direction + "!");
			return;
		}
		player.damagePlayerMove();
		player.CurrentRoom = nextRoom;
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
	}

	private void Look()
{
    Console.WriteLine(player.CurrentRoom.GetLongDescription());

    Console.WriteLine("Items in the room:");
    foreach (var itemEntry in player.CurrentRoom.Chest.items)
    {
		Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(itemEntry.Value.Description);
		Console.ForegroundColor = ConsoleColor.White;
    }

	Console.WriteLine("Enemies in the room:");
	  foreach (var enemyEntry in player.CurrentRoom.enemies)
        {
            string enemyName = enemyEntry.Key;
            Enemy enemy = enemyEntry.Value;

			Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\u001b[31m{enemy.Name}\u001b[0m, the \u001b[31m{enemy.enemyTitle}. || Health: {enemy.Health} Armor: {enemy.armor} ||");
			Console.ForegroundColor = ConsoleColor.White;
            }

        }
	private void CheckWeight()
	{
		Console.WriteLine("Items in Inventory:");
		if (player.backPack.TotalWeight() == 0)
		{
			Console.WriteLine("Inventory is empty");
		}
		else
		{
			foreach (var itemEntry in player.backPack.items)
			{
				Console.WriteLine(itemEntry.Key);
			}
		}
	}

	private void Take(Command command)
{
    if (command.HasSecondWord())
    {
        string itemName = command.SecondWord;
        if (player.TakeFromChest(itemName))
        {
            // Check if the taken item is a key
            if (itemName.ToLower().Contains("key"))
            {
                keysCollected++;
                Console.WriteLine($"You have collected {keysCollected} out of 4 keys.");
                if (keysCollected == 4)
                {
                    WinGame();
                }
            }
        }
        else
        {
            Console.WriteLine($"Could not find {itemName} in the room.");
        }
    }
    else
    {
        Console.WriteLine("Take what?");
    }
}


    private void Drop(Command command)
    {
        if (command.HasSecondWord())
        {
            string itemName = command.SecondWord;
            player.DropToChest(itemName);
        }
        else
        {
            Console.WriteLine("Drop what?");
        }
    }

private void Use(Command command)
{
    if (command.HasSecondWord())
    {
        string itemName = command.SecondWord;
        string enemyName = command.ThirdWord;

        if (player.backPack.items.ContainsKey(itemName))
        {
            if (enemyName != null)
            {
                Console.WriteLine(Use(itemName, enemyName));
            }
            else
            {
                Console.WriteLine("Specify an enemy to use the item on.");
            }
        }
        else
        {
            Console.WriteLine($"You don't have {itemName} in your inventory.");
        }
    }
    else
    {
        Console.WriteLine("Use what?");
    }
}




public string Use(string itemName, string enemyName)
{
    Item item = player.backPack.useItem(itemName);

    if (item != null)
    {
        Enemy enemy;

        if (player.CurrentRoom.enemies.TryGetValue(enemyName, out enemy))
        {
            // Deal damage to the enemy
            int damageDealt = item.Damage;
            enemy.damageEnemy(damageDealt);

            if (enemy.isDead())
            {
                player.CurrentRoom.RemoveEnemy(enemyName);
                enemy.DropLoot();
                player.health += enemy.healPlayer;

                // Check if defeated enemy is Ignis to drop Sunfire Bow
                if (enemy.Name == "Ignis")
                {
                    player.CurrentRoom.Chest.Put("sunfirebow", new Item(10, 75, "| Sunfire Bow | Condition: On fire! | Damage: 75 | "));
                    return $"\u001b[33mYou used the {itemName}\u001b[0m and defeated the \u001b[31m{enemy.Name}\u001b[0m! Dealing {damageDealt} damage. It has healed you for {enemy.healPlayer}. Ignis dropped the Sunfire Bow!";
                }
                else if (enemy.Name == "Thoros")
                {
                    player.CurrentRoom.Chest.Put("thunderclapstaff", new Item(10, 125, "| Thunderclap Staff | Condition: Thy Be Lightnin! | Damage: 125 | "));
                    player.CurrentRoom.Chest.Put("lightkey", new Item(1, 0, "Light Key | The Second of the 4 Keys"));
                    return $"\u001b[33mYou used the {itemName}\u001b[0m and defeated the \u001b[31m{enemy.Name}\u001b[0m! Dealing {damageDealt} damage. It has healed you for {enemy.healPlayer}. Thoros dropped the Thunderclap Staff and the Light Key!";
                }
                else if (enemy.Name == "Nyx")
                {
                    player.CurrentRoom.Chest.Put("eclipseblades", new Item(10, 250, "| Moonlight Dagger | Condition: GODLIKE, PEAK. | Damage: 250 | "));
                    player.CurrentRoom.Chest.Put("soulkey", new Item(1, 0, "Soul Key | The Third of the 4 Keys"));
                    return $"\u001b[33mYou used the {itemName}\u001b[0m and defeated the \u001b[31m{enemy.Name}\u001b[0m! Dealing {damageDealt} damage. It has healed you for {enemy.healPlayer}. Nyx dropped the Eclipse Blades and the Soul Key!";
                }
                else if (enemy.Name == "Luminos")
                {
                    player.CurrentRoom.Chest.Put("spiritkey", new Item(1, 0, "Spirit Key | The Last of the 4 Keys"));
                    return $"\u001b[33mYou used the {itemName}\u001b[0m and defeated the \u001b[31m{enemy.Name}\u001b[0m! Dealing {damageDealt} damage. It has healed you for {enemy.healPlayer}. Luminos dropped the Spirit Key!";
                }
                else
                {
                    return $"\u001b[33mYou used the {itemName}\u001b[0m and defeated the \u001b[31m{enemy.Name}\u001b[0m! Dealing {damageDealt} damage. It has healed you for {enemy.healPlayer}";
                }
            }
            else
            {
                // Enemy is still alive
                int damageReceived = enemy.damage; // Get the damage of the enemy
                player.damagePlayer(damageReceived);

                if (player.isAlive)
                {
                    int remainingEnemyHealth = enemy.Health;
                    return $"The {enemy.enemyTitle} attacked you and dealt {damageReceived} damage! Remaining health: {player.health}. You dealt {damageDealt} damage to the {enemy.Name} (Remaining health: {remainingEnemyHealth}).";
                }
                else
                {
                    return $"You were defeated by the {enemy.enemyTitle}. Game over!";
                }
            }
        }
        else
        {
            return $"There is no '{enemyName}' in the room to attack.";
        }
    }
    else
    {
        return $"You don't have {itemName} in your backpack.";
    }
}


public int playerdealtDMG
{
    get
    { 
        // Check if the backpack is not null and has items
        if (player.backPack != null && player.backPack.items != null)
        {
            int totalDamage = 0;

            foreach (KeyValuePair<string, Item> kvp in player.backPack.items)
            {

                Item item = kvp.Value;

                totalDamage += item.Damage;
            }

            return totalDamage;
        }

        return 0; 
    }
}
public void WinGame()
{
    Console.WriteLine("\u001b[36mCongratulations!\u001b[0m You have collected all \u001b[35mfour keys\u001b[0m and gained access to the \u001b[33mrealm\u001b[0m of the \u001b[32mLightborne\u001b[0m");
    Console.WriteLine("You have \u001b[34mslain tons\u001b[0m of \u001b[32menemies\u001b[0m, \u001b[35mi am proud of you,\u001b[0m my \u001b[36mchild.\u001b[0m");
    Console.WriteLine("You have \u001b[31mwon the game!\u001b[0m");
    gameWon = true;
}

}
