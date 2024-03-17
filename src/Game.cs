using System;

class Game
{
	// Private fields
	private Parser parser;
	private Player player;

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
		Room cavernsEntrance = new Room(" At the beginning of the Glowing Caverns");


		riverbank.AddExit("forward", beginForest);
		
		player.CurrentRoom = riverbank;

		// Create your Items here
		Item sword = new Item(10,25, "| Sword | Condition: New | Description: A new sword! | ");
		Item wand = new Item (5,25, $"| Wand | Condition: Battle Scarred |Damage: 25  | Description: A wand");
		Item stick = new Item(1,5, "A stick");
		Item voidkatana = new Item(1,100, "A Voidkatana, the weapon of the mighty Fezvoid");

		// put items into rooms
		riverbank.Chest.Put("stick", stick); 
		riverbank.Chest.Put("voidkatana",voidkatana);
		// ADD ENEMIES

		    Enemy crabbo = new Enemy(0, "Crab", player);
    		riverbank.AddEnemy("crabbo", crabbo);
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();


		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			Command command = parser.GetCommand();
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
			Console.WriteLine("TIP: Pick up Void Health, you wont die lol!");
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
		player.damagePlayer();
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
            Console.WriteLine($"\u001b[31m{enemy.Name}\u001b[0m, the \u001b[31m{enemy.enemyTitle}. || Health: {enemy.Health}");
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
            player.TakeFromChest(itemName);
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
        string enemyName = command.ThirdWord; // Check for null or handle accordingly

        Console.WriteLine(Use(itemName, enemyName));
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
            //int damage = item.Damage; // Assuming Damage property exists in Item class
            enemy.damageEnemy(item.Damage);

            if (enemy.isDead())
            {
                player.CurrentRoom.RemoveEnemy(enemyName);
                return $"\u001b[33mYou used the {itemName}\u001b[0m and defeated the \u001b[31m{enemy.Name}\u001b[0m!";

            }
            else
            {
                return $"\u001b[33mYou used the {itemName}\u001b[0m and dealt \u001b[31m{item.Damage}\u001b[0m damage to the \u001b[31m{enemy.Name}\u001b[0m. Remaining health: {enemy.Health}";

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
            // Calculate cumulative damage based on items in the backpack
            int totalDamage = 0;

            foreach (KeyValuePair<string, Item> kvp in player.backPack.items)
            {
                // Access the 'Value' property to get the 'Item' object
                Item item = kvp.Value;

                // Add the damage of the current item to the total damage
                totalDamage += item.Damage;
            }

            return totalDamage;
        }

        return 0; // Return 0 if there are no items or the backpack is null
    }
}

}
