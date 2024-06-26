using System.Collections.Generic;

class Room
{
	// Private fields
	private string description;

	internal Dictionary<string, Enemy> enemies;
	private Dictionary<string, Room> exits; // stores exits of this room.
	private Inventory chest;

	// property
	public Inventory Chest
	{
		get { return chest; }
	}

	public Room()
	{
		// a Room can handle a big inventory
		chest = new Inventory(999999);
	}

	public void AddItem(string itemName, Item item)
	{
		chest.Put(itemName, item);
	}


	// Create a room described "description". Initially, it has no exits.
	// "description" is something like "in a kitchen" or "in a court yard".
	public Room(string desc)
	{
		description = desc;
		exits = new Dictionary<string, Room>();
		chest = new Inventory(100); // Initialize the chest with a maximum weight of 100
		enemies = new Dictionary<string, Enemy>();
	}


	// Define an exit for this room.
	public void AddExit(string direction, Room neighbor)
	{
		exits.Add(direction, neighbor);
	}

	public void AddEnemy(string enemyname, Enemy enemy)
    {
        enemy.Name = enemyname;
        enemies.Add(enemyname, enemy);
    }

	// Return the description of the room.
	public string GetShortDescription()
	{
		return description;
	}

	// Return a long description of this room, in the form:
	//     You are in the kitchen.
	//     Exits: north, west
	public string GetLongDescription()
	{
		string str = "You are ";
		str += description;
		str += ".\n";
		str += GetExitString();
		return str;
	}

	// Return the room that is reached if we go from this room in direction
	// "direction". If there is no room in that direction, return null.
	public Room GetExit(string direction)
	{
		if (exits.ContainsKey(direction))
		{
			return exits[direction];
		}
		return null;
	}

	// Return a string describing the room's exits, for example
	// "Exits: north, west".
	private string GetExitString()
	{
		string str = "Exits:";

		// Build the string in a `foreach` loop.
		// We only need the keys.
		int countCommas = 0;
		foreach (string key in exits.Keys)
		{
			if (countCommas != 0)
			{
				str += ",";
			}
			str += " " + key;
			countCommas++;
		}

		return str;
	}

	public void RemoveEnemy(string enemyname)
{
    if (enemies.ContainsKey(enemyname))
    {
        enemies.Remove(enemyname);
    }
}

}
